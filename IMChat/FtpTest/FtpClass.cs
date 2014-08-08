using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FtpTest
{
    public class FtpClass
    {
        string ftpServerIP;
        string ftpUserID;
        string ftpPassword;
        FtpWebRequest reqFTP;

        //FTP地址如下： ftp://用户名：密码@FTP服务器IP或域名：FTP命令端口/路径/文件名

        public void Connecttest(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            // 根据uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP));
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        }

        #region 连接
        /// <summary>
        /// 连接
        /// </summary>
        /// <param name="path"></param>
        private void Connect(String path)//连接ftp
        {
            // 根据uri创建FtpWebRequest对象
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            // 指定数据传输类型
            reqFTP.UseBinary = true;
            // ftp用户名和密码
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
        }
        #endregion

        #region ftp登录信息
        /// <summary>
        /// ftp登录信息
        /// </summary>
        /// <param name="ftpServerIP">ftpServerIP</param>
        /// <param name="ftpUserID">ftpUserID</param>
        /// <param name="ftpPassword">ftpPassword</param>
        public void FtpUpDown(string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            this.ftpServerIP = ftpServerIP;
            this.ftpUserID = ftpUserID;
            this.ftpPassword = ftpPassword;
        }
        #endregion

        #region 获取文件列表
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="path"></param>
        /// <param name="WRMethods"></param>
        /// <returns></returns>
        private string[] GetFileList(string path, string WRMethods)//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            try
            {
                Connect(path);
                reqFTP.Method = WRMethods;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);//中文文件名
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                // to remove the trailing '\n'
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }
        public string[] GetFileList(string path)//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            return GetFileList("ftp://" + ftpServerIP + "/" + path, WebRequestMethods.Ftp.ListDirectory);
        }
        public string[] GetFileList()//上面的代码示例了如何从ftp服务器上获得文件列表
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectory);
        }
        #endregion

        #region 上传文件
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filename"></param>
        public bool Upload(string filename, string path, out string errorinfo) //上面的代码实现了从ftp服务器上载文件的功能
        {
            path = path.Replace("\\", "/");
            FileInfo fileInf = new FileInfo(filename);
            string uri = "ftp://" + path + "/" + fileInf.Name;
            Connect(uri);//连接         
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;
            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;
            // 缓冲大小设置为kb 
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流(System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();
            try
            {
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
                errorinfo = "完成";
                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("因{0},无法完成上传", ex.Message);
                return false;
            }
        }
        #endregion

        #region 续传文件
        /// <summary>
        /// 续传文件
        /// </summary>
        /// <param name="filename"></param>
        public bool Upload(string filename, long size, string path, out string errorinfo) //上面的代码实现了从ftp服务器上载文件的功能
        {
            path = path.Replace("\\", "/");
            FileInfo fileInf = new FileInfo(filename);
            //string uri = "ftp://" + path + "/" + fileInf.Name;
            string uri = "ftp://" + path;
            Connect(uri);//连接         
            // 默认为true，连接不会被关闭
            // 在一个命令之后被执行
            reqFTP.KeepAlive = false;
            // 指定执行什么命令         
            reqFTP.Method = WebRequestMethods.Ftp.AppendFile;
            // 上传文件时通知服务器文件的大小
            reqFTP.ContentLength = fileInf.Length;
            // 缓冲大小设置为kb 
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;
            // 打开一个文件流(System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();
            try
            {
                StreamReader dsad = new StreamReader(fs);
                fs.Seek(size, SeekOrigin.Begin);
                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();
                // 每次读文件流的kb
                contentLen = fs.Read(buff, 0, buffLength);
                // 流内容没有结束
                while (contentLen != 0)
                {
                    // 把内容从file stream 写入upload stream 
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }
                // 关闭两个流
                strm.Close();
                fs.Close();
                errorinfo = "完成";
                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("因{0},无法完成上传", ex.Message);
                return false;
            }
        }
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        /// <param name="errorinfo"></param>
        /// <returns></returns>
        public bool Download(string ftpfilepath, string filePath, string fileName, out string errorinfo)////上面的代码实现了从ftp服务器下载文件的功能
        {
            try
            {
                filePath = filePath.Replace("我的电脑\\", "");
                String onlyFileName = Path.GetFileName(fileName);
                string newFileName = filePath + onlyFileName;
                if (File.Exists(newFileName))
                {
                    errorinfo = string.Format("本地文件{0}已存在,无法下载", newFileName);
                    return false;
                }
                ftpfilepath = ftpfilepath.Replace("\\", "/");
                string url = "ftp://" + ftpfilepath;
                Connect(url);//连接 
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                FileStream outputStream = new FileStream(newFileName, FileMode.Create);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }
                ftpStream.Close();
                outputStream.Close();
                response.Close();
                errorinfo = "";
                return true;
            }
            catch (Exception ex)
            {
                errorinfo = string.Format("因{0},无法下载", ex.Message);
                return false;
            }
        }
        #endregion

        #region 删除文件
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        public void DeleteFileName(string fileName)
        {
            try
            {
                FileInfo fileInf = new FileInfo(fileName);
                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                Connect(uri);//连接         
                // 默认为true，连接不会被关闭
                // 在一个命令之后被执行
                reqFTP.KeepAlive = false;
                // 指定执行什么命令
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "删除错误");
            }
        }
        #endregion

        #region 在ftp上创建目录
        /// <summary>
        /// 在ftp上创建目录
        /// </summary>
        /// <param name="dirName"></param>
        public void MakeDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;
                Connect(uri);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 删除ftp上目录
        /// <summary>
        /// 删除ftp上目录
        /// </summary>
        /// <param name="dirName"></param>
        public void delDir(string dirName)
        {
            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;
                Connect(uri);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.RemoveDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 获得ftp上文件大小
        /// <summary>
        /// 获得ftp上文件大小
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public long GetFileSize(string filename)
        {
            long fileSize = 0;
            filename = filename.Replace("\\", "/");
            try
            {
                // FileInfo fileInf = new FileInfo(filename);
                //string uri1 = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                // string uri = filename;
                string uri = "ftp://" + filename;
                Connect(uri);//连接      
                reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                fileSize = response.ContentLength;
                response.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
            return fileSize;
        }
        #endregion

        #region ftp上文件改名
        /// <summary>
        /// ftp上文件改名
        /// </summary>
        /// <param name="currentFilename"></param>
        /// <param name="newFilename"></param>
        public void Rename(string currentFilename, string newFilename)
        {
            try
            {
                FileInfo fileInf = new FileInfo(currentFilename);
                string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
                Connect(uri);//连接
                reqFTP.Method = WebRequestMethods.Ftp.Rename;
                reqFTP.RenameTo = newFilename;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                //Stream ftpStream = response.GetResponseStream();
                //ftpStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region 获得文件明晰
        /// <summary>
        /// 获得文件明晰
        /// </summary>
        /// <returns></returns>
        public string[] GetFilesDetailList()
        {
            return GetFileList("ftp://" + ftpServerIP + "/", WebRequestMethods.Ftp.ListDirectoryDetails);
        }
        /// <summary>
        /// 获得文件明晰
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetFilesDetailList(string path)
        {
            path = path.Replace("\\", "/");
            return GetFileList("ftp://" + path, WebRequestMethods.Ftp.ListDirectoryDetails);
        }
        #endregion

    }

}
