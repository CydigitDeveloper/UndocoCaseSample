Undoco Games - Unity Developer Case
Bu proje, Undoco Games tarafından verilen Developer Case çalışması kapsamında geliştirilmiştir. Proje, merkezi bir yönetim sistemi altında çalışan iki farklı çocuk mini oyununu (Puzzle ve 2.5D Platformer) kapsar.

🎮 İçerik ve Özellikler
Proje, Modüler ve Genişletilebilir bir yapı hedeflenerek geliştirilmiştir.

1. Mini Game Manager (Core System)
Merkezi Yönetim: Tüm oyun geçişleri ve oyun döngüsü MiniGameManager (Singleton) üzerinden yönetilmektedir.

ScriptableObject Mimarisi: Oyun tanımları (İsim, Sahne, İkon, Açıklama) ScriptableObject veri dosyaları üzerinden dinamik olarak oluşturulur. Yeni oyun eklemek için kod yazmaya gerek yoktur.

UI Oluşturucu: Ana menüdeki butonlar, veri listesine göre otomatik generate edilir.

2. Mini Oyun: Drag & Drop Puzzle
Mekanik: 6 parçalı sürükle-bırak yapısı.

Snap Sistemi: Parçalar doğru alana yaklaştığında otomatik olarak yerleşir ve kilitlenir (Bonus: Snap Animation).

Geri Bildirim: Yanlış yerleştirmede parça başlangıç noktasına animasyonla (Lerp) geri döner.

Tamamlanma: Tüm parçalar yerleştiğinde sesli ve görsel "Tebrikler" ekranı açılır.

3. Mini Oyun: Submarine Adventure (2.5D)
Fizik Tabanlı Hareket: Rigidbody fiziği kullanılarak duvar çarpışmaları (Collision) ve hareket pürüzsüz hale getirildi.

Kamera Takibi: SmoothDamp kullanılarak yumuşak kamera geçişleri sağlandı (Bonus: Camera Easing).

Görsel Detay: Denizaltı hareket yönüne doğru döner ve dururken hafifçe süzülür (Bonus: Idle Bobbing).

Quiz Entegrasyonu: Oyun akışında mantıksal bir iyileştirme yapılarak; her sandık toplandığında oyun duraklatılır ve soru sorulur. 5 sandık ve 5 soru sonunda başarı durumuna göre (Kusursuz/Aferin/İyi) puanlama yapılır.

🔧 Mekanik Çözümler ve Teknik Detaylar
Case gereksinimlerini karşılamak ve temiz bir mimari oluşturmak için aşağıdaki teknik yaklaşımlar uygulanmıştır:

1. Puzzle Mekaniği (UI & Koordinat Sistemi)

Sürükleme işlemi için Unity EventSystem (IDragHandler, IBeginDragHandler, IEndDragHandler) arayüzleri kullanıldı.

Sorting Order Çözümü: Sürükleme başladığında parça SetAsLastSibling ile hiyerarşinin en altına (ekranın en önüne) taşınarak diğer slotların altında kalması engellendi.

Snap Algoritması: Parçalar ve Slotlar farklı UI panelleri altında olabildiği için anchoredPosition yerine World Position (transform.position) kullanılarak Vector3.Distance ile mesafe kontrolü yapıldı. Bu sayede UI hiyerarşisinden bağımsız doğru kenetlenme sağlandı.

2. Denizaltı Fiziği ve Kontrolü

Hareket: Objelerin içinden geçmeyi engellemek için Transform.Translate yerine Fizik Motoru tercih edildi. Hareket, FixedUpdate içerisinde Rigidbody.linearVelocity (Unity 6+) manipüle edilerek sağlandı.

Rotasyon: Denizaltının gittiği yöne bakması için Input vektörü Mathf.Atan2 ile açıya çevrildi ve Quaternion.Slerp ile yumuşak dönüş sağlandı.

Idle Bobbing: Oyuncu durduğunda (Input.magnitude < 0.1), Y eksenindeki hıza Mathf.Sin(Time.time) fonksiyonu eklenerek fiziksel bir süzülme efekti oluşturuldu.

3. Oyun Akışı ve Duraklatma

Tüm oyunlar Time.timeScale manipülasyonu ile duraklatılabilir yapıda kuruldu.

Quiz sistemi bir "State Machine" gibi çalışır: Sandık toplanınca oyun durur -> Quiz UI açılır -> Cevap verilince Quiz kapanır -> Oyun devam eder.

🚀 Kurulum ve Çalıştırma
Bu repoyu klonlayın veya ZIP olarak indirin.

Unity Hub üzerinden projeyi açın (Unity Version: Unity 6.2 (6000.2.7f2)).

Assets/_Game/Scenes/MainMenu sahnesini açın.

Play tuşuna basın.

Not: Sahneler arası geçişin düzgün çalışması için Build Settings altında MainMenu, PuzzleLevel ve SubmarineLevel sahnelerinin ekli olduğundan emin olun.
