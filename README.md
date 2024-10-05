# KNQTT-NET

## Phase 1: (30/09/2024 - ?)

1. Chọn chủ đề dự án

- Upwork
- tham khảo: https://www.linkedin.com/in/quang-tran-395492272/

2. Chọn những công nghệ cơ bản. sẽ dùng (technical)
   2.1. client NextJS (.ts, tailwindss, material ui, .eslint, app router)

   - Xem xét: React Hooks Form, prime/react, react skeleton (loading,...)

     2.2. 1 con API Gateway bằng ASP. NET để routing, load banlancer,...

   - Đang xem xét xem nên dùng Ocelot hay YARP

     2.3. 2 con service (trước mắt):

   - 1 con NodeJS bằng typescript (không phải NestJS)
     - Con NodeJS này đảm nhận nghiệp vụ như Auth, Blog,...? (xem xét)
     - Hãy nhớ, 1 trang web ví dụ Web bán hàng, ngoài các chức năng liên quan
       đến product, order,...
       thì còn những chức năng liên quan đến Auth, News - bài viết, nhắn tin cho shop,...
       (Sale và None-Sale)
       Tương tự, 1 trang web như Upwork cũng vậy.
   - 1 con ASP. NET Core

     - Con này sẽ đảm nhận các nghiệp vụ chính như ... ? (xem xét)

     - Con .NET này đảm nhận nghiệp vụ như ... ?

       2.4. DB: MongoDB, PostgreSQL

       2.5. Công nghệ khác:

     - OAuth2 (Facebook, Google,... )
     - Docker,
     - Docker-compose,
     - (Xem xét) CI/CD, jenkins,...
     - sonarqube,
     - ghi log (logging):
       - giai đoạn đầu ghi tạm vào file .txt hoặc .csv. Dùng các thư viện như Serilog, NLog,...
       - giai đoạn sau (xem xét) tìm cách ghi log vào Kibana (ELK) để tiện tra cứu
     - ElasticSearch (ELK) với Kibana,
     - có thể có thêm RabbitMQ, Kafka,...
     - chạy Quartz (CronJob), back-ground job,...
     - Cloud (Azure, AWS)
     - GraphQL
     - GRPC
     - Redis (mức cơ bản)
     - Cloudflare ?
     - Cloudinary để lưu trữ ảnh
     - Bảo mật hệ thống (chống DDoS, CSRF, XSS,...)
     - (Xem xét) OpenTelemetry với Jaeger, Zipkin,...
     - (Xem xét) Cloud với Azure

3. Chọn - thiết kế cơ bản UI phải làm (chưa cần code)

4. Chọn - những chức năng cơ bản phải làm (chưa cần code)

5. Thiết kế luồng (flow) cơ bản - chưa cần chi tiết

   - Kiến trúc cơ bản của hệ thống
   - Tham khảo: https://www.linkedin.com/in/quang-tran-395492272/

6. Thiết kế cơ bản Database (Có thể chỉnh sửa sau)
   6.1 PostGreSQL
   6.2 MongoDB

7. Tạo base các source code (chỉ mới là base code thôi)
   --> Nhớ theo chuẩn "clean architecture"

8. Thống nhất cách làm: log task
   --> ghi lại ngắn gọn những gì đã làm...để các anh em khác học hỏi, tra cứu,...thuận tiện

## Phase 2: Bắt đầu coding (? - ?)
