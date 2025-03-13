# RestfulApiProject

Bu proje, bir **RESTful API**'yi simüle eden bir .NET Core API uygulamasıdır. API, CRUD (Create, Read, Update, Delete) işlemleriyle ürün verilerini yönetmek için temel işlevsellik sağlar. Bu proje, HTTP metotları, HTTP status kodları, model doğrulama, hata yönetimi ve routing gibi RESTful API geliştirme konularını öğretmek amacıyla oluşturulmuştur.

## Özellikler

- **GET**, **POST**, **PUT**, **DELETE**, **PATCH** metodları desteklenir.
- **HTTP Status Kodları** standartlarına uyum sağlanır.
- Hata yönetimi (Error Handling) ile **500**, **400**, **404**, **200**, **201** hataları standart formatta gönderilir.
- Modellerde zorunlu alan kontrolü yapılır.
- Routing kullanılarak API istekleri doğru bir şekilde yönlendirilir.
- **Model Binding** işlemleri hem body hem de query string'den yapılacak şekilde örneklendirilmiştir.
- Listeleme ve sıralama işlevsellikleri eklenmiştir (Bonus özellik).

## Kurulum

### Gereksinimler

- .NET 6 veya daha yeni bir sürüm
- Visual Studio Code veya başka bir IDE
- Git (Proje sürüm kontrolü için)

### Adımlar

1. Projeyi yerel bilgisayarınıza klonlayın:
    ```bash
    git clone https://github.com/aysavsar/RestfulApiProject.git
    ```

2. Proje klasörüne gidin:
    ```bash
    cd RestfulApiProject
    ```

3. Gerekli bağımlılıkları yükleyin:
    ```bash
    dotnet restore
    ```

4. Uygulamayı başlatın:
    ```bash
    dotnet run
    ```

    Uygulama, aşağıdaki URL'lerden erişilebilir:
    - **HTTP**: `http://localhost:5037`
    - **HTTPS**: `https://localhost:7014`

## API Uç Noktaları

### 1. `GET /api/products`
- Tüm ürünleri listeler.
- **HTTP 200 OK** döner.

### 2. `GET /api/products/{id}`
- Belirli bir ürünün detaylarını getirir.
- **HTTP 404 Not Found** (Ürün bulunamazsa).

### 3. `POST /api/products`
- Yeni bir ürün oluşturur.
- **HTTP 201 Created** döner.

### 4. `PUT /api/products/{id}`
- Var olan bir ürünü günceller.
- **HTTP 200 OK** döner.

### 5. `DELETE /api/products/{id}`
- Belirli bir ürünü siler.
- **HTTP 200 OK** döner.

### 6. `PATCH /api/products/{id}`
- Belirli bir ürünü kısmi olarak günceller.
- **HTTP 200 OK** döner.

## Katkı Sağlama

Bu projeye katkıda bulunmak isterseniz, aşağıdaki adımları takip edebilirsiniz:

1. Fork yapın.
2. Yeni bir branch oluşturun (`git checkout -b feature-branch`).
3. Değişikliklerinizi yapın ve commit edin (`git commit -am 'Add new feature'`).
4. Değişikliklerinizi push edin (`git push origin feature-branch`).
5. Bir Pull Request oluşturun.


