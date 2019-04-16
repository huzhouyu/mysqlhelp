using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GeneralTools.Point
{

    /// <summary>
    /// 二维点基本实体
    /// </summary>
    public class PointBase
    {
        private double? _pointX;
        private double? _pointY;
        public PointBase()
        {

        }
        /// <summary>
        /// 实例化点集合
        /// </summary>
        /// <param name="pointX">纵坐标</param>
        /// <param name="pointY">横坐标</param>
        public PointBase(double pointX,double pointY)
        {
            this._pointX = pointX;
            this._pointY = pointY;
        }
        /// <summary>
        /// 设置X坐标
        /// </summary>
        /// <param name="pointX"></param>
        public void SetPointX(double pointX)
        {
            this._pointX = pointX;
        }
        /// <summary>
        /// 设置Y坐标
        /// </summary>
        /// <param name="pointX"></param>
        public void SetPointY(double pointX)
        {
            this._pointX = pointX;
        }
        /// <summary>
        /// 获取X坐标
        /// </summary>
        /// <returns></returns>
        public double GetPointX()
        {
            if (this._pointX == null)
            {
                throw new Exception("横坐标未设置值");
            }
            return Convert.ToDouble(this._pointX);
        }
        /// <summary>
        /// 设置Y坐标
        /// </summary>
        /// <returns></returns>
        public double GetPointY()
        {
            if (this._pointY == null)
            {
                throw new Exception("横坐标未设置值");
            }
            return Convert.ToDouble(this._pointY);
        }
    }
    public class PoinTudeBase
    {
        /// <summary>
        /// 经度
        /// </summary>
        private double? _longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        private double? _latitude { get; set; }

        public PoinTudeBase()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Longitude">经度</param>
        /// <param name="Latitude">纬度</param>
        public PoinTudeBase(double Longitude,double Latitude)
        {
            this._longitude = Longitude;
            this._latitude = Latitude;
        }
        /// <summary>
        /// 设置经度
        /// </summary>
        /// <param name="longitude">经度</param>
        public void SetLongitude(double longitude)
        {
            this._longitude =longitude;
        }
        /// <summary>
        /// 设置纬度
        /// </summary>
        /// <param name="latitude">纬度</param>
        public void SetLatitude(double latitude)
        {
            this._longitude = latitude;
        }
        /// <summary>
        /// 设置经纬度
        /// </summary>
        /// <param name="longitude">经度</param>
        /// <param name="latitude">纬度</param>
        public void SetLongAndLaTude(double longitude, double latitude)
        {
            this._longitude = longitude;
            this._longitude = latitude;
        }
        /// <summary>
        /// 获取经度
        /// </summary>
        /// <returns></returns>
        public double GetLongitude()
        {
            if (this._longitude == null)
            {
                throw new Exception("经度未设置初值");
            }
            return Convert.ToDouble(this._longitude);
        }
        /// <summary>
        /// 获取纬度
        /// </summary>
        /// <returns></returns>
        public double GetLatitude()
        {
            if (this._latitude == null)
            {
                throw new Exception("经度未设置初值");
            }
            return Convert.ToDouble(this._latitude);
        }
    }
    public class PoinTudeMeter
    {
        /// <summary>
        /// 经纬度坐标米
        /// </summary>
        private PointBase _pointBase=null;
        /// <summary>
        /// 经纬度
        /// </summary>
        private PoinTudeBase _poinTudeBase = null;
        private string _projectDefine = null;
        public PoinTudeMeter()
        {

        }

        /// <summary>
        /// 根据米实例化一个经纬度基类
        /// </summary>
        /// <param name="pointBase"></param>
        public PoinTudeMeter(PointBase pointBase)
        {
            this._pointBase = pointBase;
        }
        /// <summary>
        /// 根据经纬度实例化一个经纬度基类
        /// </summary>
        /// <param name="poinTudeBase"></param>
        public PoinTudeMeter(PoinTudeBase poinTudeBase)
        {
            this._poinTudeBase = poinTudeBase;
        }
        /// <summary>
        /// 根据米和经纬度实例化一个经纬度基类
        /// </summary>
        /// <param name="pointBase"></param>
        /// <param name="poinTudeBase"></param>
        public PoinTudeMeter(PointBase pointBase,PoinTudeBase poinTudeBase)
        {
            this._pointBase = pointBase;
            this._poinTudeBase = poinTudeBase;
        }
        /// <summary>
        /// 获取横坐标
        /// </summary>
        /// <returns></returns>
        public double GetPointX()
        {
            if (this._pointBase != null)
            {
                return this._pointBase.GetPointX();
            }
            else if(this._poinTudeBase!=null)
            {
                PoinTudeExChangePoint();
                return this._pointBase.GetPointX();
            }
            else
            {
                throw new Exception("没有米的经纬度,又没有经纬度");
            }
        }
        /// <summary>
        /// 获取纵坐标
        /// </summary>
        /// <returns></returns>
        public double GetPointY()
        {
            if (this._pointBase != null)
            {
                return this._pointBase.GetPointY();
            }
            else if (this._poinTudeBase != null)
            {
                PoinTudeExChangePoint();
                return this._pointBase.GetPointY();
            }
            else
            {
                throw new Exception("没有米的经纬度,又没有经纬度");
            }
        }

        private void PoinTudeExChangePoint()
        {

        }
    }
    public static class t
    {
        public static void ts(this PointBase t)
        {

        }
    }

}
