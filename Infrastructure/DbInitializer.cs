using System.Security.Cryptography;
using Core.Entities;
using Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class DbInitializer
{
    public static async Task SeedAsync(AppDbContext db)
    {
        if (await db.Users.AnyAsync()) return;

        var now = new DateTime(2026, 6, 7, 12, 0, 0, DateTimeKind.Utc);

        // ── Store Types ──────────────────────────────────────────
        var grocery = new StoreType { Id = SeedIds.StoreTypeGrocery, Name = "Grocery", Description = "Oziq-ovqat mahsulotlari" };
        var pharmacy = new StoreType { Id = SeedIds.StoreTypePharmacy, Name = "Pharmacy", Description = "Dorixona" };
        var restaurant = new StoreType { Id = SeedIds.StoreTypeRestaurant, Name = "Restaurant", Description = "Ovqatlanish" };
        var electronics = new StoreType { Id = SeedIds.StoreTypeElectronics, Name = "Electronics", Description = "Elektronika" };
        var clothing = new StoreType { Id = SeedIds.StoreTypeClothing, Name = "Clothing", Description = "Kiyim-kechak" };
        var furniture = new StoreType { Id = SeedIds.StoreTypeFurniture, Name = "Furniture", Description = "Mebel" };

        db.StoreTypes.AddRange(grocery, pharmacy, restaurant, electronics, clothing, furniture);

        // ── Categories ───────────────────────────────────────────
        // Grocery categories
        var catSut = new Category { Id = SeedIds.CatSutMahsulotlari, Name = "Sut mahsulotlari", Description = "Sut, qatiq, pishloq, sariyog'", StoreTypeId = SeedIds.StoreTypeGrocery };
        var catNon = new Category { Id = SeedIds.CatNonMahsulotlari, Name = "Non mahsulotlari", Description = "Non, bulochka, tort, pechenye", StoreTypeId = SeedIds.StoreTypeGrocery };
        var catIch = new Category { Id = SeedIds.CatIchimliklar, Name = "Ichimliklar", Description = "Sharbat, suv, gazli ichimliklar, choy", StoreTypeId = SeedIds.StoreTypeGrocery };
        var catGosht = new Category { Id = SeedIds.CatGoshtMahsulotlari, Name = "Go'sht mahsulotlari", Description = "Go'sht, kolbasa, tuxum", StoreTypeId = SeedIds.StoreTypeGrocery };
        var catSabz = new Category { Id = SeedIds.CatSabzavotMeva, Name = "Sabzavot va mevalar", Description = "Sabzavot, meva, ko'katlar", StoreTypeId = SeedIds.StoreTypeGrocery };
        // Pharmacy categories
        var catDori = new Category { Id = SeedIds.CatDoriVositalari, Name = "Dori vositalari", Description = "Retseptli va retseptsiz dorilar", StoreTypeId = SeedIds.StoreTypePharmacy };
        var catVit = new Category { Id = SeedIds.CatVitaminlar, Name = "Vitaminlar", Description = "Vitamin va mineral qo'shimchalar", StoreTypeId = SeedIds.StoreTypePharmacy };
        var catGig = new Category { Id = SeedIds.CatGigiena, Name = "Gigiena vositalari", Description = "Sovun, shampun, tish pastasi", StoreTypeId = SeedIds.StoreTypePharmacy };
        // Restaurant categories
        var catMilTaom = new Category { Id = SeedIds.CatMilliyTaomlar, Name = "Milliy taomlar", Description = "Osh, manti, lag'man, shashlik", StoreTypeId = SeedIds.StoreTypeRestaurant };
        var catFast = new Category { Id = SeedIds.CatFastFood, Name = "Fast food", Description = "Gamburger, hot-dog, pitsa", StoreTypeId = SeedIds.StoreTypeRestaurant };
        var catShirin = new Category { Id = SeedIds.CatShirinliklar, Name = "Shirinliklar", Description = "Tort, pirojniy, muzqaymoq", StoreTypeId = SeedIds.StoreTypeRestaurant };
        // Electronics categories
        var catTel = new Category { Id = SeedIds.CatTelefonlar, Name = "Telefonlar", Description = "Smartfon va aksessuarlar", StoreTypeId = SeedIds.StoreTypeElectronics };
        var catKom = new Category { Id = SeedIds.CatKompyuterlar, Name = "Kompyuterlar", Description = "Noutbuk, monitor, orgtexnika", StoreTypeId = SeedIds.StoreTypeElectronics };
        var catMai = new Category { Id = SeedIds.CatMaishiyTexnika, Name = "Maishiy texnika", Description = "Changyutkich, mikroto'lqinli pech", StoreTypeId = SeedIds.StoreTypeElectronics };

        db.Categories.AddRange(catSut, catNon, catIch, catGosht, catSabz, catDori, catVit, catGig, catMilTaom, catFast, catShirin, catTel, catKom, catMai);

        // ── Users ────────────────────────────────────────────────
        var admin = new User { Id = SeedIds.UserAdmin, FullName = "Admin", Phone = "+998901234567", PasswordHash = HashPassword("admin123"), Role = UserRole.Admin, IsActive = true, CreatedAt = now };
        var partner1 = new User { Id = SeedIds.UserPartner1, FullName = "Bakirjon", Phone = "+998901234568", PasswordHash = HashPassword("partner123"), Role = UserRole.Partner, IsActive = true, CreatedAt = now };
        var partner2 = new User { Id = SeedIds.UserPartner2, FullName = "Kamoliddin", Phone = "+998901234571", PasswordHash = HashPassword("partner123"), Role = UserRole.Partner, IsActive = true, CreatedAt = now };
        var courier1 = new User { Id = SeedIds.UserCourier1, FullName = "Shokirjon", Phone = "+998901234569", PasswordHash = HashPassword("courier123"), Role = UserRole.Courier, IsActive = true, CreatedAt = now };
        var courier2 = new User { Id = SeedIds.UserCourier2, FullName = "Azamat", Phone = "+998901234572", PasswordHash = HashPassword("courier123"), Role = UserRole.Courier, IsActive = true, CreatedAt = now };
        var client1 = new User { Id = SeedIds.UserClient1, FullName = "Valijon", Phone = "+998901234570", PasswordHash = HashPassword("client123"), Role = UserRole.Client, IsActive = true, CreatedAt = now };
        var client2 = new User { Id = SeedIds.UserClient2, FullName = "Madina", Phone = "+998901234573", PasswordHash = HashPassword("client123"), Role = UserRole.Client, IsActive = true, CreatedAt = now };
        var client3 = new User { Id = SeedIds.UserClient3, FullName = "Jahongir", Phone = "+998901234574", PasswordHash = HashPassword("client123"), Role = UserRole.Client, IsActive = true, CreatedAt = now };

        db.Users.AddRange(admin, partner1, partner2, courier1, courier2, client1, client2, client3);

        // ── Stores ───────────────────────────────────────────────
        var store1 = new Store { Id = SeedIds.StoreBekobodMarket, Name = "Bekobod Market", Description = "Eng arzon narxlar, keng assortiment", Address = "Bekobod, Navoiy ko'ch, 12", Latitude = 40.2206, Longitude = 69.2697, Phone = "+998901111111", OwnerId = SeedIds.UserPartner1, StoreTypeId = SeedIds.StoreTypeGrocery, IsActive = true, CreatedAt = now };
        var store2 = new Store { Id = SeedIds.StoreYangiBozor, Name = "Yangi Bozor", Description = "Tabiiy va sifatli mahsulotlar", Address = "Bekobod, Mustaqillik ko'ch, 3", Latitude = 40.2215, Longitude = 69.2685, Phone = "+998901111112", OwnerId = SeedIds.UserPartner2, StoreTypeId = SeedIds.StoreTypeGrocery, IsActive = true, CreatedAt = now };
        var store3 = new Store { Id = SeedIds.StoreDorixona1, Name = "Dorixona 1", Description = "Barcha dori vositalari mavjud", Address = "Bekobod, Mustaqillik ko'ch, 5", Latitude = 40.2210, Longitude = 69.2700, Phone = "+998902222221", OwnerId = SeedIds.UserPartner1, StoreTypeId = SeedIds.StoreTypePharmacy, IsActive = true, CreatedAt = now };
        var store4 = new Store { Id = SeedIds.StoreSoglikDorixona, Name = "Sog'lik Dorixonasi", Description = "Tabiiy dorilar va vitaminlar", Address = "Bekobod, I.Karimov ko'ch, 10", Latitude = 40.2200, Longitude = 69.2710, Phone = "+998902222222", OwnerId = SeedIds.UserPartner2, StoreTypeId = SeedIds.StoreTypePharmacy, IsActive = true, CreatedAt = now };
        var store5 = new Store { Id = SeedIds.StoreOshMarkazi, Name = "Osh Markazi", Description = "Mazali milliy taomlar 24/7", Address = "Bekobod, A.Navoiy ko'ch, 8", Latitude = 40.2220, Longitude = 69.2680, Phone = "+998903333331", OwnerId = SeedIds.UserPartner1, StoreTypeId = SeedIds.StoreTypeRestaurant, IsActive = true, CreatedAt = now };
        var store6 = new Store { Id = SeedIds.StoreKebabHouse, Name = "Kebab House", Description = "Shashlik va gril taomlari", Address = "Bekobod, Gagarin ko'ch, 22", Latitude = 40.2230, Longitude = 69.2670, Phone = "+998903333332", OwnerId = SeedIds.UserPartner1, StoreTypeId = SeedIds.StoreTypeRestaurant, IsActive = true, CreatedAt = now };
        var store7 = new Store { Id = SeedIds.StoreMilliyTaomlar, Name = "Milliy Taomlar", Description = "An'anaviy o'zbek taomlari", Address = "Bekobod, Bog' ko'ch, 15", Latitude = 40.2225, Longitude = 69.2690, Phone = "+998903333333", OwnerId = SeedIds.UserPartner2, StoreTypeId = SeedIds.StoreTypeRestaurant, IsActive = true, CreatedAt = now };
        var store8 = new Store { Id = SeedIds.StoreTexnoShop, Name = "Texno Shop", Description = "Zamonaviy texnikalar", Address = "Bekobod, I.Karimov ko'ch, 1", Latitude = 40.2200, Longitude = 69.2715, Phone = "+998904444441", OwnerId = SeedIds.UserPartner1, StoreTypeId = SeedIds.StoreTypeElectronics, IsActive = true, CreatedAt = now };
        var store9 = new Store { Id = SeedIds.StoreSmartLife, Name = "Smart Life", Description = "Aqlli texnologiyalar", Address = "Bekobod, Navoiy ko'ch, 50", Latitude = 40.2210, Longitude = 69.2705, Phone = "+998904444442", OwnerId = SeedIds.UserPartner2, StoreTypeId = SeedIds.StoreTypeElectronics, IsActive = true, CreatedAt = now };

        db.Stores.AddRange(store1, store2, store3, store4, store5, store6, store7, store8, store9);

        // ── Products (Grocery – Bekobod Market) ──────────────────
        db.Products.AddRange(
            new Product { Id = SeedIds.ProdSut1L, Name = "Sut 1L", Description = "Yangi sigir suti 1 litr", Price = 12000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSutMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdQatiq, Name = "Qatiq 500ml", Description = "Chuchuk qatiq 500 ml", Price = 8000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSutMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdPishloq, Name = "Pishloq 200g", Description = "Golland pishlog'i 200 gramm", Price = 25000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSutMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdYog, Name = "Sariyog' 200g", Description = "Tabiiy sariyog' 200 gramm", Price = 18000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSutMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdNon, Name = "Non", Description = "Jizza noni", Price = 3000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatNonMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdPirojniy, Name = "Pirojniy", Description = "Shokoladli pirojniy", Price = 5000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatNonMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdCocaCola, Name = "Coca-Cola 1.5L", Description = "Gazli ichimlik 1.5 litr", Price = 15000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatIchimliklar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdSharbat, Name = "Sharbat 1L", Description = "Olma sharbati 1 litr", Price = 10000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatIchimliklar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdChoy, Name = "Ko'k choy 250g", Description = "Baliqchi ko'k choy 250 gramm", Price = 22000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatIchimliklar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdKofe, Name = "Nescafe 100g", Description = "Eritma kofe 100 gramm", Price = 35000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatIchimliklar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdGosht, Name = "Mol go'shti 1kg", Description = "Yangi mol go'shti 1 kilogramm", Price = 85000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatGoshtMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdKolbasa, Name = "Kolbasa 500g", Description = "Doktor kolbasa 500 gramm", Price = 45000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatGoshtMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdTuxum, Name = "Tuxum 10 dona", Description = "Tovuq tuxumi 10 dona karton", Price = 15000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatGoshtMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdKartoshka, Name = "Kartoshka 1kg", Description = "Sariq kartoshka 1 kilogramm", Price = 5000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSabzavotMeva, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdPomidor, Name = "Pomidor 1kg", Description = "Suvli pomidor 1 kilogramm", Price = 12000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSabzavotMeva, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdOlma, Name = "Olma 1kg", Description = "Qizil olma 1 kilogramm", Price = 10000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSabzavotMeva, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdBanan, Name = "Banan 1kg", Description = "Jahon banani 1 kilogramm", Price = 18000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatSabzavotMeva, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdGuruch, Name = "Guruch 1kg", Description = "Devzira guruch 1 kilogramm", Price = 14000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatNonMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdMakaron, Name = "Makaron 500g", Description = "Spagetti makaron 500 gramm", Price = 7000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatNonMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdUn, Name = "Un 1kg", Description = "Bug'doy uni 1 kilogramm", Price = 6000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatNonMahsulotlari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdShakar, Name = "Shakar 1kg", Description = "Oq shakar 1 kilogramm", Price = 11000, Unit = 1, StoreId = SeedIds.StoreBekobodMarket, CategoryId = SeedIds.CatNonMahsulotlari, IsAvailable = true, CreatedAt = now }
        );

        // ── Products (Pharmacy) ──────────────────────────────────
        db.Products.AddRange(
            new Product { Id = SeedIds.ProdParasetamol, Name = "Parasetamol 500mg", Description = "Isti tushiruvchi 20 tabletka", Price = 5000, Unit = 1, StoreId = SeedIds.StoreDorixona1, CategoryId = SeedIds.CatDoriVositalari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdIbuprofen, Name = "Ibuprofen 400mg", Description = "Og'riq qoldiruvchi 30 tabletka", Price = 8000, Unit = 1, StoreId = SeedIds.StoreDorixona1, CategoryId = SeedIds.CatDoriVositalari, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdVitaminC, Name = "Vitamin C 1000mg", Description = "Askorbin kislota 20 tabletka", Price = 15000, Unit = 1, StoreId = SeedIds.StoreDorixona1, CategoryId = SeedIds.CatVitaminlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdVitaminD, Name = "Vitamin D3 2000IU", Description = "Vitamin D3 60 kapsula", Price = 35000, Unit = 1, StoreId = SeedIds.StoreDorixona1, CategoryId = SeedIds.CatVitaminlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdSovun, Name = "Sovun 100g", Description = "Antibakterial sovun 100 gramm", Price = 5000, Unit = 1, StoreId = SeedIds.StoreDorixona1, CategoryId = SeedIds.CatGigiena, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdShampun, Name = "Shampun 400ml", Description = "Head & Shoulders shampun 400 ml", Price = 35000, Unit = 1, StoreId = SeedIds.StoreDorixona1, CategoryId = SeedIds.CatGigiena, IsAvailable = true, CreatedAt = now }
        );

        // ── Products (Restaurant) ────────────────────────────────
        db.Products.AddRange(
            new Product { Id = SeedIds.ProdOsh, Name = "Palov", Description = "An'anaviy o'zbek palovi 1 porsiya", Price = 25000, Unit = 1, StoreId = SeedIds.StoreOshMarkazi, CategoryId = SeedIds.CatMilliyTaomlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdManti, Name = "Manti", Description = "Go'shtli manti 5 dona", Price = 22000, Unit = 1, StoreId = SeedIds.StoreOshMarkazi, CategoryId = SeedIds.CatMilliyTaomlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdShashlik, Name = "Shashlik", Description = "Mol go'shtidan shashlik 1 porsiya", Price = 18000, Unit = 1, StoreId = SeedIds.StoreKebabHouse, CategoryId = SeedIds.CatMilliyTaomlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdLagman, Name = "Lag'mon", Description = "Uy lag'moni 1 porsiya", Price = 20000, Unit = 1, StoreId = SeedIds.StoreMilliyTaomlar, CategoryId = SeedIds.CatMilliyTaomlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdGamburger, Name = "Gamburger", Description = "Katta gamburger kartoshka bilan", Price = 22000, Unit = 1, StoreId = SeedIds.StoreKebabHouse, CategoryId = SeedIds.CatFastFood, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdHotDog, Name = "Hot-dog", Description = "Classic hot-dog", Price = 12000, Unit = 1, StoreId = SeedIds.StoreKebabHouse, CategoryId = SeedIds.CatFastFood, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdMedoviy, Name = "Medoviy tort", Description = "Asalli tort 1kg", Price = 55000, Unit = 1, StoreId = SeedIds.StoreMilliyTaomlar, CategoryId = SeedIds.CatShirinliklar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdNapoleon, Name = "Napoleon tort", Description = "Napoleon torti 1kg", Price = 60000, Unit = 1, StoreId = SeedIds.StoreMilliyTaomlar, CategoryId = SeedIds.CatShirinliklar, IsAvailable = true, CreatedAt = now }
        );

        // ── Products (Electronics) ───────────────────────────────
        db.Products.AddRange(
            new Product { Id = SeedIds.ProdIPhone, Name = "iPhone 15", Description = "Apple iPhone 15 128GB", Price = 8500000, Unit = 1, StoreId = SeedIds.StoreTexnoShop, CategoryId = SeedIds.CatTelefonlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdBuds, Name = "AirPods Pro", Description = "Apple AirPods Pro 2-avlod", Price = 1500000, Unit = 1, StoreId = SeedIds.StoreTexnoShop, CategoryId = SeedIds.CatTelefonlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdNoutbuk, Name = "MacBook Air M3", Description = "Apple MacBook Air M3 13-inch", Price = 12000000, Unit = 1, StoreId = SeedIds.StoreSmartLife, CategoryId = SeedIds.CatKompyuterlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdMonitor, Name = "Monitor 27\"", Description = "Samsung 27\" 4K monitor", Price = 3500000, Unit = 1, StoreId = SeedIds.StoreSmartLife, CategoryId = SeedIds.CatKompyuterlar, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdChangyutkich, Name = "Changyutkich", Description = "Samsung changyutkich 2000W", Price = 1200000, Unit = 1, StoreId = SeedIds.StoreTexnoShop, CategoryId = SeedIds.CatMaishiyTexnika, IsAvailable = true, CreatedAt = now },
            new Product { Id = SeedIds.ProdMikroTolqinli, Name = "Mikroto'lqinli pech", Description = "Samsung 20L mikroto'lqinli pech", Price = 950000, Unit = 1, StoreId = SeedIds.StoreSmartLife, CategoryId = SeedIds.CatMaishiyTexnika, IsAvailable = true, CreatedAt = now }
        );

        // ── Couriers ─────────────────────────────────────────────
        var courierProfile1 = new Courier { Id = SeedIds.Courier1, IsAvailable = true, Rating = 4.8m, TotalDeliveries = 320, VehicleType = "Mototsikl", UserId = SeedIds.UserCourier1 };
        var courierProfile2 = new Courier { Id = SeedIds.Courier2, IsAvailable = true, Rating = 4.5m, TotalDeliveries = 150, VehicleType = "Velosiped", UserId = SeedIds.UserCourier2 };
        db.Couriers.AddRange(courierProfile1, courierProfile2);

        // ── Orders ───────────────────────────────────────────────
        var order1 = new Order { Id = SeedIds.Order1, OrderNumber = "B24-20260606-0001", Status = OrderStatus.Delivered, TotalAmount = 49000, DeliveryAddress = "Bekobod, Gagarin ko'ch, 15", ClientId = SeedIds.UserClient1, CourierId = SeedIds.UserCourier1, StoreId = SeedIds.StoreBekobodMarket, CreatedAt = now.AddDays(-2).AddHours(-3), DeliveredAt = now.AddDays(-2).AddHours(-1) };
        var order2 = new Order { Id = SeedIds.Order2, OrderNumber = "B24-20260606-0002", Status = OrderStatus.Delivered, TotalAmount = 33000, DeliveryAddress = "Bekobod, Oqtepa mahalla, 7", ClientId = SeedIds.UserClient1, CourierId = SeedIds.UserCourier1, StoreId = SeedIds.StoreBekobodMarket, CreatedAt = now.AddDays(-1).AddHours(-5), DeliveredAt = now.AddDays(-1).AddHours(-3) };
        var order3 = new Order { Id = SeedIds.Order3, OrderNumber = "B24-20260607-0003", Status = OrderStatus.OutForDelivery, TotalAmount = 25000, DeliveryAddress = "Bekobod, Navoiy ko'ch, 45", ClientId = SeedIds.UserClient2, CourierId = SeedIds.UserCourier2, StoreId = SeedIds.StoreOshMarkazi, CreatedAt = now.AddHours(-2) };
        var order4 = new Order { Id = SeedIds.Order4, OrderNumber = "B24-20260607-0004", Status = OrderStatus.New, TotalAmount = 8500000, DeliveryAddress = "Bekobod, I.Karimov ko'ch, 2", ClientId = SeedIds.UserClient3, CourierId = null, StoreId = SeedIds.StoreTexnoShop, CreatedAt = now.AddMinutes(-30) };
        var order5 = new Order { Id = SeedIds.Order5, OrderNumber = "B24-20260607-0005", Status = OrderStatus.Preparing, TotalAmount = 38000, DeliveryAddress = "Bekobod, Mustaqillik ko'ch, 20", ClientId = SeedIds.UserClient2, CourierId = null, StoreId = SeedIds.StoreDorixona1, CreatedAt = now.AddMinutes(-15) };
        var order6 = new Order { Id = SeedIds.Order6, OrderNumber = "B24-20260607-0006", Status = OrderStatus.Cancelled, TotalAmount = 22000, DeliveryAddress = "Bekobod, Bog' ko'ch, 8", ClientId = SeedIds.UserClient3, CourierId = null, StoreId = SeedIds.StoreKebabHouse, CreatedAt = now.AddHours(-4) };

        db.Orders.AddRange(order1, order2, order3, order4, order5, order6);

        // ── Order Items ──────────────────────────────────────────
        db.OrderItems.AddRange(
            // Order 1: Delivered – grocery
            new OrderItem { Id = SeedIds.OrderItem1, ProductName = "Sut 1L", UnitPrice = 12000, Quantity = 2, TotalPrice = 24000, OrderId = SeedIds.Order1, ProductId = SeedIds.ProdSut1L },
            new OrderItem { Id = SeedIds.OrderItem2, ProductName = "Non", UnitPrice = 3000, Quantity = 3, TotalPrice = 9000, OrderId = SeedIds.Order1, ProductId = SeedIds.ProdNon },
            new OrderItem { Id = SeedIds.OrderItem3, ProductName = "Coca-Cola 1.5L", UnitPrice = 15000, Quantity = 1, TotalPrice = 15000, OrderId = SeedIds.Order1, ProductId = SeedIds.ProdCocaCola },
            new OrderItem { Id = SeedIds.OrderItem4, ProductName = "Tuxum 10 dona", UnitPrice = 15000, Quantity = 1, TotalPrice = 15000, OrderId = SeedIds.Order1, ProductId = SeedIds.ProdTuxum },
            // Order 2: Delivered – grocery
            new OrderItem { Id = SeedIds.OrderItem5, ProductName = "Pishloq 200g", UnitPrice = 25000, Quantity = 1, TotalPrice = 25000, OrderId = SeedIds.Order2, ProductId = SeedIds.ProdPishloq },
            new OrderItem { Id = SeedIds.OrderItem6, ProductName = "Non", UnitPrice = 3000, Quantity = 1, TotalPrice = 3000, OrderId = SeedIds.Order2, ProductId = SeedIds.ProdNon },
            new OrderItem { Id = SeedIds.OrderItem7, ProductName = "Sharbat 1L", UnitPrice = 10000, Quantity = 1, TotalPrice = 10000, OrderId = SeedIds.Order2, ProductId = SeedIds.ProdSharbat },
            // Order 3: Out for delivery – restaurant
            new OrderItem { Id = SeedIds.OrderItem8, ProductName = "Palov", UnitPrice = 25000, Quantity = 1, TotalPrice = 25000, OrderId = SeedIds.Order3, ProductId = SeedIds.ProdOsh },
            // Order 4: New – iPhone
            new OrderItem { Id = SeedIds.OrderItem9, ProductName = "iPhone 15", UnitPrice = 8500000, Quantity = 1, TotalPrice = 8500000, OrderId = SeedIds.Order4, ProductId = SeedIds.ProdIPhone },
            // Order 5: Preparing – pharmacy
            new OrderItem { Id = SeedIds.OrderItem10, ProductName = "Vitamin C 1000mg", UnitPrice = 15000, Quantity = 2, TotalPrice = 30000, OrderId = SeedIds.Order5, ProductId = SeedIds.ProdVitaminC },
            new OrderItem { Id = SeedIds.OrderItem11, ProductName = "Sovun 100g", UnitPrice = 5000, Quantity = 1, TotalPrice = 5000, OrderId = SeedIds.Order5, ProductId = SeedIds.ProdSovun },
            // Order 6: Cancelled
            new OrderItem { Id = SeedIds.OrderItem12, ProductName = "Gamburger", UnitPrice = 22000, Quantity = 1, TotalPrice = 22000, OrderId = SeedIds.Order6, ProductId = SeedIds.ProdGamburger }
        );

        // ── Reviews ──────────────────────────────────────────────
        db.Reviews.AddRange(
            new Review { Id = SeedIds.Review1, Rating = 5, Comment = "Juda tez yetkazildi, mahsulotlar yangi!", CreatedAt = now.AddDays(-2), UserId = SeedIds.UserClient1, StoreId = SeedIds.StoreBekobodMarket, OrderId = SeedIds.Order1, CourierId = SeedIds.Courier1 },
            new Review { Id = SeedIds.Review2, Rating = 4, Comment = "Yaxshi, lekin non biroz qattiq edi", CreatedAt = now.AddDays(-1), UserId = SeedIds.UserClient1, StoreId = SeedIds.StoreBekobodMarket, OrderId = SeedIds.Order2 },
            new Review { Id = SeedIds.Review3, Rating = 5, Comment = "Osh ajoyib! Qayta buyurtma beraman", CreatedAt = now.AddHours(-1), UserId = SeedIds.UserClient2, StoreId = SeedIds.StoreOshMarkazi, OrderId = SeedIds.Order3, CourierId = SeedIds.Courier2 },
            new Review { Id = SeedIds.Review4, Rating = 5, Comment = "Yangi va sifatli dorilar, rahmat!", CreatedAt = now, UserId = SeedIds.UserClient2, StoreId = SeedIds.StoreDorixona1 },
            new Review { Id = SeedIds.Review5, Rating = 3, Comment = "Mahsulot yaxshi, yetkazish tez edi", CreatedAt = now, UserId = SeedIds.UserClient3, StoreId = SeedIds.StoreKebabHouse }
        );

        await db.SaveChangesAsync();
    }

    private static string HashPassword(string password)
    {
        var salt = RandomNumberGenerator.GetBytes(16);
        var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, 100000, HashAlgorithmName.SHA256, 32);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }
}
