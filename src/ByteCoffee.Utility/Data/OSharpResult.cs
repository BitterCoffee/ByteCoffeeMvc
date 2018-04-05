﻿namespace ByteCoffee.Utility.Data
{
    /// <summary>
    /// OSharp结果基类
    /// </summary>
    /// <typeparam name="TResultType"></typeparam>
    public abstract class OSharpResult<TResultType> : OSharpResult<TResultType, object>, ITEResult<TResultType>
    {
        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType}"/>类型的新实例
        /// </summary>
        protected OSharpResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType}"/>类型的新实例
        /// </summary>
        protected OSharpResult(TResultType type)
            : this(type, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType}"/>类型的新实例
        /// </summary>
        protected OSharpResult(TResultType type, string message)
            : this(type, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType}"/>类型的新实例
        /// </summary>
        protected OSharpResult(TResultType type, string message, object data)
            : base(type, message, data)
        { }
    }

    /// <summary>
    /// OSharp结果基类
    /// </summary>
    /// <typeparam name="TResultType">结果类型</typeparam>
    /// <typeparam name="TData">结果数据类型</typeparam>
    public abstract class OSharpResult<TResultType, TData> : ITEResult<TResultType, TData>
    {
        protected string _message;

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected OSharpResult()
            : this(default(TResultType))
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected OSharpResult(TResultType type)
            : this(type, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected OSharpResult(TResultType type, string message)
            : this(type, message, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OSharpResult{TResultType,TData}"/>类型的新实例
        /// </summary>
        protected OSharpResult(TResultType type, string message, TData data)
        {
            ResultType = type;
            _message = message;
            Data = data;
        }

        /// <summary>
        /// 获取或设置 结果类型
        /// </summary>
        public TResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public virtual string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 获取或设置 结果数据
        /// </summary>
        public TData Data { get; set; }
    }
}