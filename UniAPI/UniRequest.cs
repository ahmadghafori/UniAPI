namespace UniAPI
{
    public class UniRequest<TRequest>
    {
        /// <summary>
        /// مسیر یا Query
        /// - برای REST: endpoint مثل "/users"
        /// - برای GraphQL: متن Query/Mutation
        /// - برای gRPC: اسم متد
        /// </summary>
        public string PathOrQuery { get; set; } = "";

        /// <summary>
        /// Payload اصلی
        /// - برای REST: body
        /// - برای GraphQL: متغیرها (variables)
        /// - برای gRPC: message object
        /// </summary>
        public TRequest? Body { get; set; }

        /// <summary>
        /// پارامترها
        /// - برای REST: به QueryString اضافه میشه
        /// - برای GraphQL: به عنوان Variables map میشه
        /// - برای gRPC: می‌تونه Metadata یا input باشه
        /// </summary>
        public Dictionary<string, object>? Parameters { get; set; }

        /// <summary>
        /// هدرها
        /// - برای REST: مستقیم به HttpClient headers اضافه میشه
        /// - برای GraphQL: به HttpHeaders اضافه میشه
        /// - برای gRPC: به Metadata اضافه میشه
        /// </summary>
        public Dictionary<string, string>? Headers { get; set; }

        /// <summary>
        /// نوع متد HTTP برای REST و مبنا برای دیگر پروتکل‌ها
        /// 
        /// قوانین Mapping داخلی:
        /// - REST: مقادیر HttpMethodType مستقیماً استفاده می‌شوند.
        /// - GraphQL:
        ///     - GET => Query
        ///     - POST, PUT, DELETE, PATCH => Mutation
        /// - gRPC: به رفتار خود تابع بستگی داره و این مقدار هیچ کاربردی درون GRPC ندارد
        /// </summary>
        public HttpMethod Method { get; set; } = HttpMethod.Post;

        /// <summary>
        /// تنظیمات درخواست (Timeout, Retry, Logging, ...)
        /// </summary>
        public UniRequestConfig? Config { get; set; }
    }

}
