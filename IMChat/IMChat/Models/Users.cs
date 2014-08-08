using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IMChat.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IMChat.Models
{
    public class Users
    {
        [BsonId]
        public ObjectId UserId { get; set; }

        [Required]
        [Display(Name = "用户名")]
        [StringLength(12, ErrorMessage = "必须包含{2}个字符", MinimumLength = 5)]
        public string UserName { get; set; }
        [Required]
        [DataType(System.ComponentModel.DataAnnotations.DataType.Password)]
        [Display(Name = "密码")]
        public string Salt { get; set; }
        [Display(Name = "昵称")]
        public string NickName { get; set; }

        //安全登录
        //实际对比值
        public string Hash { get; set; }

        public int Status { get; set; }

        //用户其余信息
        [Display(Name = "个性签名")]
        [StringLength(50, ErrorMessage = "长度超出限制")]
        public string SignName { get; set; }//个性签名
        [Display(Name = "头像")]
        public string HeadImage { get; set; }//头像
        [Display(Name = "手机号")]
        [StringLength(11, ErrorMessage = "手机号格式不正确", MinimumLength = 11)]
        public string Mobile { get; set; }//手机号
        [Display(Name = "邮箱")]
        public string Email { get; set; }//邮箱
        [Display(Name = "性别")]
        public string Gender { get; set; }//性别
        [Display(Name = "扩展信息")]
        public ExtendUser ExtenderInfo { get; set; }//扩展信息
    }
}