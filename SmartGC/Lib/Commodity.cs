using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartGC.Lib
{
    /// <summary>
    /// 兑换商品类
    /// </summary>
    public class Commodity
    {
        string id;
        /// <summary>
        /// 商品编号
        /// </summary>
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        string name;
        /// <summary>
        /// 商品名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string manufacturer;
        /// <summary>
        /// 生产厂家
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        string type;
        /// <summary>
        /// 商品类型
        /// </summary>
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
        int score;
        /// <summary>
        /// 消耗积分
        /// </summary>
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
    }
}
