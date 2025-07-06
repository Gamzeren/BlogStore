#  📲 Blog Projesi - Katmanlı Mimari ve Akıllı Yorum Sistemi
## 🔧 Projede Kullanılan Mimari Katmanlar
Bu projede **katmanlı mimari** kullanılmıştır. Katmanlar, kodun düzenli, okunabilir ve sürdürülebilir olması için farklı sorumluluklara ayrılmıştır.
### 1. Presentation (Sunum) Katmanı 🖥️
🔹 Kullanıcı arayüzü, Controller’lar ve View’ler burada yer alır. </br>
🔹 Kullanıcı ile etkileşim ve görsel sunum buradan sağlanır. </br>
### 2. Entity (Varlık) Katmanı 🗂️
🔹 Veritabanı tablolarını temsil eden modeller (AppUser, Article, Category, Tag). </br>
🔹 Sadece veri yapıları içerir, iş mantığı yoktur. </br>
### 3. DataAccess (Veri Erişim) Katmanı 💾
🔹Entity Framework kullanılarak veritabanı işlemleri yapılır. </br>
🔹CRUD ve özel sorgular bu katmanda yazılır. </br>
### 4. Business (İş) Katmanı ⚙️
🔹İş mantığı ve kurallar burada uygulanır. </br>
🔹 Servisler, manager sınıfları burada yer alır. </br>
🔹 Bağımlılık kayıtları Extension sınıfı ile yönetilir. </br>
## 💎 AJAX ile Yorum İşlemleri
📡 Yorumlar sayfa yenilenmeden AJAX ile gönderilir. </br>
🚫 Giriş yapmayan kullanıcılar için yorum alanı gizlenir, giriş yapmaları istenir. </br>
## 🔐 URL Güvenliği: Slug Kullanımı
🔒 ID yerine okunabilir slug kullanılarak URL güvenliği artırıldı. </br>
🌐 Örnek URL: /Article/ArticleDetail/dijital-sanatta-trendler-nft-ve-metaverse-etkisi </br>
## 👤 Identity ile Kullanıcı Yönetimi
🔑 ASP.NET Identity kullanılarak güvenli kullanıcı işlemleri yapıldı. </br>
🛡️ Kayıt, giriş, parola sıfırlama ve yetkilendirme sağlanır. </br>
## 🤖 Akıllı Yorum Sistemi - ToxicBERT
⚠️ ToxicBERT ile toksik yorumlar tespit edilir. </br>
🌍 Türkçe yorumlar önce İngilizceye çevrilir (Helsinki-NLP/opus-mt-tr-en), sonra analiz edilir. </br>
🚫 Zararlı yorumlar engellenir. </br>
## 📑 Proje Sayfaları
### 🌐 Admin Paneli
📊 Dashboard: Kategori makale dağılımı grafik, son 4 makale ve son 5 yorum. </br>
🗃️ Makale Listem: Kullanıcı makaleleri kart görünümünde listelenir. </br>
➕ Yeni Makale: Başlık, görsel URL, kategori ve içerik ile makale ekleme. </br>
👤 Profilim: Bilgi güncelleme sonrası oturum kapanır. </br>
### 💠 UI Paneli
🏠 Ana Sayfa: Tüm makaleler listelenir, detayda yazar ve yorumlar gösterilir. </br>
📚 Kategoriler: Tüm kategoriler ve ilgili makaleler listelenir. </br>
🖋️ Yazarlar: Yazarlar ve yazdıkları makaleler. </br>
🔐 Giriş Yap: Kullanıcı admin paneline erişir ve yorum yapabilir. </br>
## 🌀 BusinessLayer'da Extension Pattern
🧩 Bağımlılık kayıtları Extension sınıfına taşındı. </br>
🧹 Program.cs temiz ve modüler hale geldi. </br>
## 🖼 Proje Görselleri 
### 🎀 UI Paneli
<img width="1891" height="916" alt="Image" src="https://github.com/user-attachments/assets/eca8d3dd-3e31-4396-b366-ce945cf3837d" />

<img width="1877" height="917" alt="Image" src="https://github.com/user-attachments/assets/c086afe8-5956-48b5-9061-dabddb81c498" />

<img width="1883" height="916" alt="Image" src="https://github.com/user-attachments/assets/b3c9a2c6-7c68-460e-bd58-03f7a6b8bd16" />

<img width="1881" height="915" alt="Image" src="https://github.com/user-attachments/assets/f9797c8d-a84e-4a35-b69b-07a6db5c17c5" />

<img width="1881" height="912" alt="Image" src="https://github.com/user-attachments/assets/a42e6b0e-baca-4751-bde4-675e670d0329" />

<img width="1916" height="962" alt="Image" src="https://github.com/user-attachments/assets/aa87c42a-f17a-420a-acf4-1b85dbd3c27e" />

<img width="1182" height="921" alt="Image" src="https://github.com/user-attachments/assets/d804a936-949b-4297-a74b-afe90883bf74" />

<img width="1017" height="227" alt="Image" src="https://github.com/user-attachments/assets/1a819291-c552-4ea8-90f3-f58e053f27bc" />

<img width="476" height="308" alt="Image" src="https://github.com/user-attachments/assets/c530ebad-6d83-4d41-8ece-4443f3347852" />

<img width="643" height="425" alt="Image" src="https://github.com/user-attachments/assets/522be820-a195-44ab-9820-1942438ab080" />

### 💻 Admin Paneli
<img width="1877" height="912" alt="Image" src="https://github.com/user-attachments/assets/bdbc0d8e-a0e1-4735-8400-ad146e12b99c" />

<img width="1900" height="731" alt="Image" src="https://github.com/user-attachments/assets/6895e959-cd62-4735-9327-050544ac7bef" />

<img width="1876" height="886" alt="Image" src="https://github.com/user-attachments/assets/d1873b42-7940-437b-b564-0db69ff59a7f" />

<img width="1880" height="913" alt="Image" src="https://github.com/user-attachments/assets/759682c5-c457-4994-9fad-6d34852c11ed" />

<img width="226" height="218" alt="Image" src="https://github.com/user-attachments/assets/6203c3c4-2271-4fa7-8fa7-8e8b7dfa7f57" />

<img width="442" height="575" alt="Image" src="https://github.com/user-attachments/assets/9744118d-bb90-4a8c-b208-7e26d87b2c39" />

<img width="447" height="808" alt="Image" src="https://github.com/user-attachments/assets/be6dd9b5-f963-4374-a377-e5e95f8b37f4" />



