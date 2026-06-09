namespace Infrastructure;

internal static class SeedIds
{
    // StoreTypes (a)
    public static readonly Guid StoreTypeGrocery = Guid.Parse("a0000000-0000-0000-0000-000000000001");
    public static readonly Guid StoreTypePharmacy = Guid.Parse("a0000000-0000-0000-0000-000000000002");
    public static readonly Guid StoreTypeRestaurant = Guid.Parse("a0000000-0000-0000-0000-000000000003");
    public static readonly Guid StoreTypeElectronics = Guid.Parse("a0000000-0000-0000-0000-000000000004");
    public static readonly Guid StoreTypeClothing = Guid.Parse("a0000000-0000-0000-0000-000000000005");
    public static readonly Guid StoreTypeFurniture = Guid.Parse("a0000000-0000-0000-0000-000000000006");

    // Users (b)
    public static readonly Guid UserAdmin = Guid.Parse("b0000000-0000-0000-0000-000000000001");
    public static readonly Guid UserPartner1 = Guid.Parse("b0000000-0000-0000-0000-000000000002");
    public static readonly Guid UserPartner2 = Guid.Parse("b0000000-0000-0000-0000-000000000003");
    public static readonly Guid UserCourier1 = Guid.Parse("b0000000-0000-0000-0000-000000000004");
    public static readonly Guid UserCourier2 = Guid.Parse("b0000000-0000-0000-0000-000000000005");
    public static readonly Guid UserClient1 = Guid.Parse("b0000000-0000-0000-0000-000000000006");
    public static readonly Guid UserClient2 = Guid.Parse("b0000000-0000-0000-0000-000000000007");
    public static readonly Guid UserClient3 = Guid.Parse("b0000000-0000-0000-0000-000000000008");

    // Stores (c)
    public static readonly Guid StoreBekobodMarket = Guid.Parse("c0000000-0000-0000-0000-000000000001");
    public static readonly Guid StoreYangiBozor = Guid.Parse("c0000000-0000-0000-0000-000000000002");
    public static readonly Guid StoreDorixona1 = Guid.Parse("c0000000-0000-0000-0000-000000000003");
    public static readonly Guid StoreSoglikDorixona = Guid.Parse("c0000000-0000-0000-0000-000000000004");
    public static readonly Guid StoreOshMarkazi = Guid.Parse("c0000000-0000-0000-0000-000000000005");
    public static readonly Guid StoreKebabHouse = Guid.Parse("c0000000-0000-0000-0000-000000000006");
    public static readonly Guid StoreMilliyTaomlar = Guid.Parse("c0000000-0000-0000-0000-000000000007");
    public static readonly Guid StoreTexnoShop = Guid.Parse("c0000000-0000-0000-0000-000000000008");
    public static readonly Guid StoreSmartLife = Guid.Parse("c0000000-0000-0000-0000-000000000009");
    public static readonly Guid StoreModaOlami = Guid.Parse("c0000000-0000-0000-0000-00000000000a");
    public static readonly Guid StoreUyBezagi = Guid.Parse("c0000000-0000-0000-0000-00000000000b");

    // Categories (d)
    public static readonly Guid CatSutMahsulotlari = Guid.Parse("d0000000-0000-0000-0000-000000000001");
    public static readonly Guid CatNonMahsulotlari = Guid.Parse("d0000000-0000-0000-0000-000000000002");
    public static readonly Guid CatIchimliklar = Guid.Parse("d0000000-0000-0000-0000-000000000003");
    public static readonly Guid CatGoshtMahsulotlari = Guid.Parse("d0000000-0000-0000-0000-000000000004");
    public static readonly Guid CatSabzavotMeva = Guid.Parse("d0000000-0000-0000-0000-000000000005");
    public static readonly Guid CatDoriVositalari = Guid.Parse("d0000000-0000-0000-0000-000000000006");
    public static readonly Guid CatVitaminlar = Guid.Parse("d0000000-0000-0000-0000-000000000007");
    public static readonly Guid CatGigiena = Guid.Parse("d0000000-0000-0000-0000-000000000008");
    public static readonly Guid CatMilliyTaomlar = Guid.Parse("d0000000-0000-0000-0000-000000000009");
    public static readonly Guid CatFastFood = Guid.Parse("d0000000-0000-0000-0000-00000000000a");
    public static readonly Guid CatShirinliklar = Guid.Parse("d0000000-0000-0000-0000-00000000000b");
    public static readonly Guid CatTelefonlar = Guid.Parse("d0000000-0000-0000-0000-00000000000c");
    public static readonly Guid CatKompyuterlar = Guid.Parse("d0000000-0000-0000-0000-00000000000d");
    public static readonly Guid CatMaishiyTexnika = Guid.Parse("d0000000-0000-0000-0000-00000000000e");

    // Products (e)
    public static readonly Guid ProdSut1L = Guid.Parse("e0000000-0000-0000-0000-000000000001");
    public static readonly Guid ProdNon = Guid.Parse("e0000000-0000-0000-0000-000000000002");
    public static readonly Guid ProdCocaCola = Guid.Parse("e0000000-0000-0000-0000-000000000003");
    public static readonly Guid ProdQatiq = Guid.Parse("e0000000-0000-0000-0000-000000000004");
    public static readonly Guid ProdPishloq = Guid.Parse("e0000000-0000-0000-0000-000000000005");
    public static readonly Guid ProdSharbat = Guid.Parse("e0000000-0000-0000-0000-000000000006");
    public static readonly Guid ProdPirojniy = Guid.Parse("e0000000-0000-0000-0000-000000000007");
    public static readonly Guid ProdTuxum = Guid.Parse("e0000000-0000-0000-0000-000000000008");
    public static readonly Guid ProdYog = Guid.Parse("e0000000-0000-0000-0000-000000000009");
    public static readonly Guid ProdGosht = Guid.Parse("e0000000-0000-0000-0000-00000000000a");
    public static readonly Guid ProdKolbasa = Guid.Parse("e0000000-0000-0000-0000-00000000000b");
    public static readonly Guid ProdOlma = Guid.Parse("e0000000-0000-0000-0000-00000000000c");
    public static readonly Guid ProdBanan = Guid.Parse("e0000000-0000-0000-0000-00000000000d");
    public static readonly Guid ProdKartoshka = Guid.Parse("e0000000-0000-0000-0000-00000000000e");
    public static readonly Guid ProdPomidor = Guid.Parse("e0000000-0000-0000-0000-00000000000f");
    public static readonly Guid ProdGuruch = Guid.Parse("e0000000-0000-0000-0000-000000000010");
    public static readonly Guid ProdMakaron = Guid.Parse("e0000000-0000-0000-0000-000000000011");
    public static readonly Guid ProdUn = Guid.Parse("e0000000-0000-0000-0000-000000000012");
    public static readonly Guid ProdShakar = Guid.Parse("e0000000-0000-0000-0000-000000000013");
    public static readonly Guid ProdChoy = Guid.Parse("e0000000-0000-0000-0000-000000000014");
    public static readonly Guid ProdKofe = Guid.Parse("e0000000-0000-0000-0000-000000000015");
    // Pharmacy
    public static readonly Guid ProdParasetamol = Guid.Parse("e0000000-0000-0000-0000-000000000016");
    public static readonly Guid ProdIbuprofen = Guid.Parse("e0000000-0000-0000-0000-000000000017");
    public static readonly Guid ProdVitaminC = Guid.Parse("e0000000-0000-0000-0000-000000000018");
    public static readonly Guid ProdVitaminD = Guid.Parse("e0000000-0000-0000-0000-000000000019");
    public static readonly Guid ProdSovun = Guid.Parse("e0000000-0000-0000-0000-00000000001a");
    public static readonly Guid ProdShampun = Guid.Parse("e0000000-0000-0000-0000-00000000001b");
    // Restaurant
    public static readonly Guid ProdOsh = Guid.Parse("e0000000-0000-0000-0000-00000000001c");
    public static readonly Guid ProdManti = Guid.Parse("e0000000-0000-0000-0000-00000000001d");
    public static readonly Guid ProdShashlik = Guid.Parse("e0000000-0000-0000-0000-00000000001e");
    public static readonly Guid ProdLagman = Guid.Parse("e0000000-0000-0000-0000-00000000001f");
    public static readonly Guid ProdGamburger = Guid.Parse("e0000000-0000-0000-0000-000000000020");
    public static readonly Guid ProdHotDog = Guid.Parse("e0000000-0000-0000-0000-000000000021");
    public static readonly Guid ProdMedoviy = Guid.Parse("e0000000-0000-0000-0000-000000000022");
    public static readonly Guid ProdNapoleon = Guid.Parse("e0000000-0000-0000-0000-000000000023");
    // Electronics
    public static readonly Guid ProdIPhone = Guid.Parse("e0000000-0000-0000-0000-000000000024");
    public static readonly Guid ProdBuds = Guid.Parse("e0000000-0000-0000-0000-000000000026");
    public static readonly Guid ProdNoutbuk = Guid.Parse("e0000000-0000-0000-0000-000000000027");
    public static readonly Guid ProdMonitor = Guid.Parse("e0000000-0000-0000-0000-000000000028");
    public static readonly Guid ProdChangyutkich = Guid.Parse("e0000000-0000-0000-0000-000000000029");
    public static readonly Guid ProdMikroTolqinli = Guid.Parse("e0000000-0000-0000-0000-00000000002a");

    // Couriers (f)
    public static readonly Guid Courier1 = Guid.Parse("f0000000-0000-0000-0000-000000000001");
    public static readonly Guid Courier2 = Guid.Parse("f0000000-0000-0000-0000-000000000002");

    // Orders (g)
    public static readonly Guid Order1 = Guid.Parse("a0010000-0000-0000-0000-000000000001");
    public static readonly Guid Order2 = Guid.Parse("a0010000-0000-0000-0000-000000000002");
    public static readonly Guid Order3 = Guid.Parse("a0010000-0000-0000-0000-000000000003");
    public static readonly Guid Order4 = Guid.Parse("a0010000-0000-0000-0000-000000000004");
    public static readonly Guid Order5 = Guid.Parse("a0010000-0000-0000-0000-000000000005");
    public static readonly Guid Order6 = Guid.Parse("a0010000-0000-0000-0000-000000000006");

    // OrderItems (h)
    public static readonly Guid OrderItem1 = Guid.Parse("a0020000-0000-0000-0000-000000000001");
    public static readonly Guid OrderItem2 = Guid.Parse("a0020000-0000-0000-0000-000000000002");
    public static readonly Guid OrderItem3 = Guid.Parse("a0020000-0000-0000-0000-000000000003");
    public static readonly Guid OrderItem4 = Guid.Parse("a0020000-0000-0000-0000-000000000004");
    public static readonly Guid OrderItem5 = Guid.Parse("a0020000-0000-0000-0000-000000000005");
    public static readonly Guid OrderItem6 = Guid.Parse("a0020000-0000-0000-0000-000000000006");
    public static readonly Guid OrderItem7 = Guid.Parse("a0020000-0000-0000-0000-000000000007");
    public static readonly Guid OrderItem8 = Guid.Parse("a0020000-0000-0000-0000-000000000008");
    public static readonly Guid OrderItem9 = Guid.Parse("a0020000-0000-0000-0000-000000000009");
    public static readonly Guid OrderItem10 = Guid.Parse("a0020000-0000-0000-0000-00000000000a");
    public static readonly Guid OrderItem11 = Guid.Parse("a0020000-0000-0000-0000-00000000000b");
    public static readonly Guid OrderItem12 = Guid.Parse("a0020000-0000-0000-0000-00000000000c");

    // Reviews (i)
    public static readonly Guid Review1 = Guid.Parse("a0030000-0000-0000-0000-000000000001");
    public static readonly Guid Review2 = Guid.Parse("a0030000-0000-0000-0000-000000000002");
    public static readonly Guid Review3 = Guid.Parse("a0030000-0000-0000-0000-000000000003");
    public static readonly Guid Review4 = Guid.Parse("a0030000-0000-0000-0000-000000000004");
    public static readonly Guid Review5 = Guid.Parse("a0030000-0000-0000-0000-000000000005");
}
