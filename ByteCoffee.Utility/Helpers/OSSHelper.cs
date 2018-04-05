using Aliyun.OSS;
using Aliyun.OSS.Common;
using ByteCoffee.Utility.Data;
using System;
using System.IO;
using System.Web;

namespace ByteCoffee.Utility.Helpers
{
    /// <summary>
    /// Oss����
    /// </summary>
    public class OssConfig
    {
        /// <summary>
        /// Gets the access identifier.
        /// </summary>
        /// <value>
        /// The access identifier.
        /// </value>
        public string AccessId { get; private set; }

        /// <summary>
        /// Gets the access key.
        /// </summary>
        /// <value>
        /// The access key.
        /// </value>
        public string AccessKey { get; private set; }

        /// <summary>
        /// Gets the endpoint.
        /// </summary>
        /// <value>
        /// The endpoint.
        /// </value>
        public string Endpoint { get; private set; }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <param name="IsDebug">if set to <c>true</c> [is debug].</param>
        /// <returns></returns>
        public static OssConfig GetConfig(bool IsDebug)
        {
            var config = new OssConfig();
            if (IsDebug)
            {
                config.AccessId = "kvVXCoEgEwcg53IO";
                config.AccessKey = "YykpGV4qOnUB4IFWym7q5yHzN0cFuE";
                config.Endpoint = "http://oss-cn-hangzhou.aliyuncs.com";
            }
            else
            {
                config.AccessId = "kvVXCoEgEwcg53IO";
                config.AccessKey = "YykpGV4qOnUB4IFWym7q5yHzN0cFuE";
                config.Endpoint = "http://oss-cn-hangzhou.aliyuncs.com";//http://oss-cn-hzjbp-a-internal.aliyuncs.com
            }
            return config;
        }
    }

    /// <summary>
    /// Oss����
    /// </summary>
    public class OssHelper
    {
        private static readonly OssConfig OssConfig = OssConfig.GetConfig(false);
        private static readonly OssClient OssClient = new OssClient(OssConfig.Endpoint, OssConfig.AccessId, OssConfig.AccessKey);

        /// <summary>
        /// ����������bucket��key����Object
        /// </summary>
        /// <param name="bucketName">bucket����</param>
        /// <param name="key">OSS�洢·��������</param>
        /// <param name="file">�ϴ��ļ�</param>
        /// <param name="etag">��Դ�����ļǺ�</param>
        /// <returns></returns>
        public static OperationResult PutObject(string bucketName, string key, HttpPostedFileBase file, out string etag)
        {
            etag = string.Empty;
            try
            {
                var metadata = new ObjectMetadata { CacheControl = "No-Cache", ContentLength = file.ContentLength, ContentType = file.ContentType };
                etag = OssClient.PutObject(bucketName, key, file.InputStream, metadata).ETag;
                //var accs = OssClient.GetBucketAcl(bucketName);
                //var fileUrl = !accs.Grants.Any() ? OssClient.GeneratePresignedUri(bucketName, key, DateTime.Now.AddMinutes(5)).AbsoluteUri : string.Format("http://{0}.oss-cn-hzjbp-a-internal.aliyuncs.com/{1}", bucketName, key);
                var fileUrl = string.Format("http://{0}.oss-cn-hangzhou.aliyuncs.com/{1}", bucketName, key);
                return new OperationResult(OperationResultType.Success, string.Empty, fileUrl);
            }
            catch (OssException ex)
            { return new OperationResult(OperationResultType.Error, string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId)); }
            catch (Exception ex)
            { return new OperationResult(OperationResultType.Error, string.Format("Failed with error info: {0}", ex.Message)); }
        }

        public static OperationResult PutObject(string bucketName, string key, HttpPostedFileBase file)
        {
            try
            {
                var metadata = new ObjectMetadata { CacheControl = "No-Cache", ContentLength = file.ContentLength, ContentType = file.ContentType };
                OssClient.PutObject(bucketName, key, file.InputStream, metadata);
                //var accs = OssClient.GetBucketAcl(bucketName);
                //var fileUrl = !accs.Grants.Any() ? OssClient.GeneratePresignedUri(bucketName, key, DateTime.Now.AddMinutes(5)).AbsoluteUri : string.Format("http://{0}.oss-cn-hzjbp-a-internal.aliyuncs.com/{1}", bucketName, key);
                var fileUrl = string.Format("http://{0}.oss-cn-hangzhou.aliyuncs.com/{1}", bucketName, key);
                return new OperationResult(OperationResultType.Success, string.Empty, fileUrl);
            }
            catch (OssException ex)
            { return new OperationResult(OperationResultType.Error, string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId)); }
            catch (Exception ex)
            { return new OperationResult(OperationResultType.Error, string.Format("Failed with error info: {0}", ex.Message)); }
        }

        public static OperationResult PutObject(string bucketName, string key, string filePath)
        {
            try
            {
                OssClient.PutObject(bucketName, key, filePath);
                //var accs = OssClient.GetBucketAcl(bucketName);
                //var fileUrl = !accs.Grants.Any() ? OssClient.GeneratePresignedUri(bucketName, key, DateTime.Now.AddMinutes(5)).AbsoluteUri : string.Format("http://{0}.oss-cn-hzjbp-a-internal.aliyuncs.com/{1}", bucketName, key);
                var fileUrl = string.Format("http://{0}.oss-cn-hangzhou.aliyuncs.com/{1}", bucketName, key);
                return new OperationResult(OperationResultType.Success, string.Empty, fileUrl);
            }
            catch (OssException ex)
            { return new OperationResult(OperationResultType.Error, string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID:{2}\tHostID:{3}", ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId)); }
            catch (Exception ex)
            { return new OperationResult(OperationResultType.Error, string.Format("Failed with error info: {0}", ex.Message)); }
        }

        /// <summary>
        /// ����������bucket��key��ȡObject
        /// </summary>
        /// <param name="bucketName">bucket����</param>
        /// <param name="key">OSS�洢·��������</param>
        /// <param name="fileToDownload">����·�����ļ�</param>
        /// <param name="mime">mime����ֵ</param>
        /// <returns></returns>
        public static OperationResult GetObject(String bucketName, string key, string fileToDownload, out string mime)
        {
            var o = OssClient.GetObject(bucketName, key);
            mime = o.Metadata.ContentType;

            using (var requestStream = o.Content)
            {
                var buf = new byte[1024];
                var fs = File.Open(fileToDownload, FileMode.OpenOrCreate);
                int len;
                while ((len = requestStream.Read(buf, 0, 1024)) != 0)
                { fs.Write(buf, 0, len); }
                fs.Close();
                return new OperationResult(OperationResultType.Success);
            }
        }

        /// <summary>
        /// ����������bucket��keyɾ��Object
        /// </summary>
        /// <param name="bucketName">bucket����</param>
        /// <param name="key">OSS�洢·��������</param>
        public static void DeleteObject(string bucketName, string key)
        { OssClient.DeleteObject(bucketName, key); }
    }
}