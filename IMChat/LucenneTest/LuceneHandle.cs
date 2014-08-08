using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;

namespace LucenneTest
{
    public static class LuceneHandle
    {
        #region 简单代码
        ////1、由于索引是存放在硬盘里的，所以先定义一个索引的目录

        ///// <summary>
        ///// 索引存放目录
        ///// </summary>
        //protected string IndexDic
        //{
        //    get { return AppDomain.CurrentDomain.BaseDirectory + "/IndexDic"; }
        //}

        ////2、创建索引器把要索引的内容写入到指定目录
        ////       索引器的构造函数参数说明：
        ////IndexDic是索引存放目录
        ////PanGuAnalyzer是盘古解析器（由于默认的解析器解析能力不强，所以替换为这个）
        ////IsCreate是索引创建方式（true：重新新建索引，false：从旧的索引执行追加）
        ////Lucene.Net.Index.IndexWriter.MaxFieldLength.LIMITED是文件长度是否限制
        //protected IndexWriter writer
        //{
        //    get
        //    {
        //        return new IndexWriter(IndexDic, new PanGuAnalyzer(), true, Lucene.Net.Index.IndexWriter.MaxFieldLength.LIMITED);
        //    }
        //}

        ////3、创建索引Document和往文档写入索引内容
        ////        Document是索引文档，可以理解成数据库里的记录
        ////Field是索引文档里的字段，可以直接理解成数据库里的字段
        ////Field构造函数说明：
        ////第一个是字段名称（实例里是Title,Content,AddTime）。
        ////第二个是字段的存储方式（Field.Store.YES：进行存储，Filed.Store.No：不进行存储）有些字段值比较大，可以选择No不存储，对字段进行存储是为了检索的时候对某些字段进行提取。
        ////第三个是是否索引（Field.Index.ANALYZED：索引， Field.Index.NOT_ANALYZED：非索引）
        //public void AddIndex(IndexWriter writer, string title, string text, string date)
        //{
        //    try
        //    {
        //        Document docu = new Document();
        //        docu.Add(new Field("title", title, Field.Store.YES, Field.Index.ANALYZED));
        //        docu.Add(new Field("text", text, Field.Store.YES, Field.Index.ANALYZED));
        //        docu.Add(new Field("date", date, Field.Store.YES, Field.Index.ANALYZED));
        //        writer.AddDocument(docu);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //} 
        #endregion

        public static void CreateIndex(string index, string title, string body)
        {
            string indexPath = AppDomain.CurrentDomain.BaseDirectory + "lucene/";
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isUpdate = IndexReader.IndexExists(directory);
            if (isUpdate)
            {
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            var writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);
            //9.1.1 通过设置IndexWriter的参数优化索引建立 
            //setMaxBufferedDocs(int maxBufferedDocs) 
            //控制写入一个新的segment前内存中保存的document的数目，设置较大的数目可以加快建索引速度，默认为10。 
            //setMaxMergeDocs(int maxMergeDocs) 
            //控制一个segment中可以保存的最大document数目，值较小有利于追加索引的速度，默认Integer.MAX_VALUE，无需修改。 
            //setMergeFactor(int mergeFactor) 
            //控制多个segment合并的频率，值较大时建立索引速度较快，默认是10，可以在建立索引时设置为100。 

            //是否为符合文件
            writer.SetUseCompoundFile(false);

            writer.SetMaxBufferedDocs(100);
            writer.SetMaxMergeDocs(int.MaxValue);
            writer.SetMergeFactor(100);

            //为避免重复索引，所以先删除number=i的记录，再重新添加
            writer.DeleteDocuments(new Term("number", index));

            var document = new Document();
            //只有对需要全文检索的字段才ANALYZED
            document.Add(new Field("number", index, Field.Store.YES, Field.Index.NOT_ANALYZED));
            document.Add(new Field("title", title, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field("body", body, Field.Store.YES, Field.Index.ANALYZED,
                                   Field.TermVector.WITH_POSITIONS_OFFSETS));
            writer.AddDocument(document);
            writer.Commit();
            //writer.Optimize();
            writer.Close();
            directory.Close(); //不要忘了Close，否则索引结果搜不到;
        }

        //简单搜索
        public static string Search(string kw)
        {
            string indexPath = AppDomain.CurrentDomain.BaseDirectory + "lucene/";
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            var searcher = new IndexSearcher(reader);

            #region 基本查询TermQuery
            //首先介绍最基本的查询，如果你想执行一个这样的查询：“在content域中包含‘lucene’的document”
            TermQuery term = new TermQuery(new Term("title", kw));

            //排序
            SortField sf = new SortField("number", SortField.INT, true);
            Sort st = new Sort(sf);

            //过滤


            TopFieldDocs result = searcher.Search(term, null, 3, st);
            ScoreDoc[] docs = result.scoreDocs;
            #endregion

            #region 普通条件查询BooleanQuery
            //如果你想这么查询：“在content域中包含java或perl的document”，
            //那么你可以建立两个TermQuery并把它们用BooleanQuery连接起来： 
            //TermQuery termQuery1 = new TermQuery(new Term("title", "java"));
            //TermQuery termQuery2 = new TermQuery(new Term("title", "perl"));
            //BooleanQuery booleanQuery = new BooleanQuery();
            //booleanQuery.Add(termQuery1, BooleanClause.Occur.SHOULD);
            //booleanQuery.Add(termQuery2, BooleanClause.Occur.SHOULD);
            //Hits result = searcher.Search(booleanQuery);
            #endregion

            #region 通配查询WildcardQuery
            //如果你想对某单词进行通配符查询，你可以用WildcardQuery，
            //通配符包括’?’匹配一个任意字符和’*’匹配零个或多个任意字符，
            //例如你搜索’use*’，你可能找到’useful’或者’useless’： 
            //Query query = new WildcardQuery(new Term("title", kw + "*"));
            //Hits result = searcher.Search(query);
            #endregion

            #region PhraseQuery距离查询
            //你可能对中日关系比较感兴趣，想查找‘中’和‘日’挨得比较近（5个字的距离内）的文章，超过这个距离的不予考虑
            //PhraseQuery query = new PhraseQuery();
            //query.SetSlop(5);
            //query.Add(new Term("title", "北"));
            //query.Add(new Term("title", "京"));
            //Hits result = searcher.Search(query);
            #endregion

            #region 其他查询
            //7.1.5 PrefixQuery 
            //如果你想搜以‘中’开头的词语，你可以用PrefixQuery

            //7.1.6 FuzzyQuery 
            //FuzzyQuery用来搜索相似的term，使用Levenshtein算法。假设你想搜索跟‘wuzza’相似的词语

            //7.1.7 RangeQuery 
            //另一个常用的Query是RangeQuery，你也许想搜索时间域从20060101到20060130之间的document
            #endregion

            #region QueryParser
            //看了这么多Query，你可能会问：“不会让我自己组合各种Query吧，太麻烦了！”
            //当然不会，lucene提供了一种类似于SQL语句的查询语句，我们姑且叫它lucene语句，通过它，
            //你可以把各种查询一句话搞定，lucene会自动把它们查分成小块交给相应Query执行。下面我们对应每种Query演示一下： 
            //TermQuery可以用“field:key”方式，例如“content:lucene”。 
            //BooleanQuery中‘与’用‘+’，‘或’用‘ ’，例如“content:java contenterl”。 
            //WildcardQuery仍然用‘?’和‘*’，例如“content:use*”。 
            //PhraseQuery用‘~’，例如“content:"中日"~5”。 
            //PrefixQuery用‘*’，例如“中*”。 
            //FuzzyQuery用‘~’，例如“content: wuzza ~”。 
            //RangeQuery用‘[]’或‘{}’，前者表示闭区间，后者表示开区间，例如“time:[20060101 TO 20060130]”，注意TO区分大小写。 
            //你可以任意组合query string，完成复杂操作，例如“标题或正文包括lucene，
            //并且时间在20060101到20060130之间的文章”可以表示为：
            //“+ (title:lucene content:lucene) +time:[20060101 TO 20060130]”。代码如下： 

            //QueryParser parser = new QueryParser("content", new StandardAnalyzer());
            //Query query = parser.Parse("+(title:lucene content:lucene)+time:[20060101 TO 20060130]");
            //Hits result = searcher.Search(query);
            #endregion

            string str = "";
            for (int i = 0; i < docs.Length; i++)
            {
                Document doc = reader.Document(docs[i].doc);
                str += doc.Get("title") + "/////" + doc.Get("body") + "<br />";
            }
            reader.Close();
            return str;
        }

        //盘古分词
        public static string PanGu(string str)
        {
            string analysStr = "";
            PanGuAnalyzer pangu = new PanGuAnalyzer();
            TokenStream ts = pangu.TokenStream("", new StringReader(str));
            Lucene.Net.Analysis.Token token = null;
            while (ts.IncrementToken())
                analysStr += ts.ToString();//token.TermText() + "/";
            return analysStr.Trim('/');
        }


        //删除索引
        public static bool DelIndex(string kw)
        {
            string indexPath = AppDomain.CurrentDomain.BaseDirectory + "lucene/";
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, false);
            var searcher = new IndexSearcher(reader);

            //首先介绍最基本的查询，如果你想执行一个这样的查询：“在content域中包含‘lucene’的document”
            //TermQuery term = new TermQuery(new Term("title", kw));
            bool result = reader.DeleteDocuments(new Term("title", kw)) > 0;
            reader.Commit();
            reader.Close();
            return result;
        }

        //文件整合
        public static void Combine()
        {
            string indexPath = AppDomain.CurrentDomain.BaseDirectory + "lucene/";
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            bool isUpdate = IndexReader.IndexExists(directory);
            if (isUpdate)
            {
                //如果索引目录被锁定（比如索引过程中程序异常退出），则首先解锁
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            var writer = new IndexWriter(directory, new PanGuAnalyzer(), !isUpdate, Lucene.Net.Index.IndexWriter.MaxFieldLength.UNLIMITED);

            writer.Optimize();
            writer.Close();
            directory.Close();
        }


        //索引实例解析
        public static void IndexReaderTest()
        {
            IndexWriter writer = new IndexWriter(FSDirectory.Open(new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "lucene/")), new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.LIMITED);
            #region •用于索引文档
            //影响打分的标准化因子(normalization factor)部分，对文档的打分分两个部分，一部分是索引阶段计算的，与查询语句无关，一部分是搜索阶段计算的，与查询语句相关。 
            Similarity similar = Similarity.GetDefault();
            //保存段信息，和segments_N中的信息几乎一一对应。 
            SegmentInfos segmentInfos = new SegmentInfos();

            IndexFileDeleter deleter; //此对象不是用来删除文档的，而是用来管理索引文件的。 
            Lock writeLock; //每一个索引文件夹只能打开一个IndexWriter，所以需要锁。

            HashSet<SegmentInfo> segmentsToOptimize = new HashSet<SegmentInfo>(); //保存正在最优化(optimize)的段信息。当调用optimize的时候，当前所有的段信息加入此Set，此后新生成的段并不参与此次最优化。  
            #endregion

            #region •用于合并段，在合并段的文章中将详细描述
            SegmentInfos localRollbackSegmentInfos;
            HashSet<SegmentInfo> mergingSegments = new HashSet<SegmentInfo>();//保存正在合并的段，以防止合并期间再次选中被合并。 
            MergePolicy mergePolicy = new LogByteSizeMergePolicy(writer);//合并策略，也即选取哪些段来进行合并。
            MergeScheduler mergeScheduler = new ConcurrentMergeScheduler();//段合并器，背后有一个线程负责合并。 
            LinkedList<MergePolicy.OneMerge> pendingMerges = new LinkedList<MergePolicy.OneMerge>();//等待被合并的任务
            HashSet<MergePolicy.OneMerge> runningMerges = new HashSet<MergePolicy.OneMerge>();//正在被合并的任务 
            List<MergePolicy.OneMerge> mergeExceptions = new List<MergePolicy.OneMerge>();
            long mergeGen;


            //和段合并有关的一些参数有：
            //•mergeFactor：当大小几乎相当的段的数量达到此值的时候，开始合并。 
            //•minMergeSize：所有大小小于此值的段，都被认为是大小几乎相当，一同参与合并。 
            //•maxMergeSize：当一个段的大小大于此值的时候，就不再参与合并。 
            //•maxMergeDocs：当一个段包含的文档数大于此值的时候，就不再参与合并。

            #endregion

            #region •为保持索引完整性，一致性和事务性
            SegmentInfos rollbackSegmentInfos; //当IndexWriter对索引进行了添加，删除文档操作后，可以调用commit将修改提交到文件中去，也可以调用rollback取消从上次commit到此时的修改。 
            //SegmentInfos localRollbackSegmentInfos; //此段信息主要用于将其他的索引文件夹合并到此索引文件夹的时候，为防止合并到一半出错可回滚所保存的原来的段信息。  
            #endregion

            #region •一些配置
            long writeLockTimeout; //获得锁的时间超时。当超时的时候，说明此索引文件夹已经被另一个IndexWriter打开了。 
            int termIndexInterval; //同tii和tis文件中的indexInterval。 
            #endregion

            

        }

    }
}