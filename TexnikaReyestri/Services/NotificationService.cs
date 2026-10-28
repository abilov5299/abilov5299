using TexnikaReyestri.Data;

namespace TexnikaReyestri.Services
{
    /// <summary>Bildiriş qaydaları: proqram açılanda və hər saat yoxlanılır. Eyni bildiriş gündə bir dəfə yaranır.</summary>
    public static class NotificationService
    {
        public static async Task YoxlaAsync()
        {
            int yaxin = await SistemRepo.ParamAsync("BildirisYaxin", 3), gec = await SistemRepo.ParamAsync("BildirisGec", 0);
            int sorgu = await SistemRepo.ParamAsync("BildirisSorgu", 30), paket = await SistemRepo.ParamAsync("BildirisPaket", 7);
            string bugun = Vaxt.Iso(DateTime.Today);
            var siyahi = new List<(string acar, string nov, string basliq, string metn, string zavod, long id)>();
            await using (var con = await LocalDb.AcAsync())
            {
                using var cmd = con.CreateCommand();
                cmd.CommandText = @"
SELECT Id, Marka, ZavodNo, TemirZavodu, GozlenilenBitme, CAST(julianday($b)-julianday(GozlenilenBitme) AS INTEGER), Status, Teminat, BaslamaTarixi
FROM Texnikalar WHERE Silinib=0 AND ($z IS NULL OR TemirZavodu=$z) AND Status IN ('Qəbul edilib','Təmirdə') AND IFNULL(FaktikiBitme,'')=''
  AND (GozlenilenBitme <= date($b, '+' || $y || ' days') OR Teminat='Təminat gözləyir');";
                cmd.Parameters.AddWithValue("$b", bugun); cmd.Parameters.AddWithValue("$y", yaxin); cmd.Parameters.AddWithValue("$z", (object)Session.ZavodFiltri ?? DBNull.Value);
                await using var r = await cmd.ExecuteReaderAsync();
                while (await r.ReadAsync())
                {
                    long id = r.GetInt64(0); string ad = $"{r[1]} ({r[2]})", zv = r[3] as string;
                    if (!r.IsDBNull(5))
                    {
                        int fark = r.GetInt32(5);
                        if (fark > gec) siyahi.Add(($"late:{id}:{bugun}", "late", "Təmir gecikir", $"{ad}: gözlənilən bitmə tarixi {Vaxt.Tarix(r[4])} idi, {fark} gün keçib", zv, id));
                        else if (fark <= 0) siyahi.Add(($"soon:{id}:{r[4]}", "soon", "Son tarix yaxınlaşır", $"{ad}: təmirin bitməsinə {-fark} gün qalıb", zv, id));
                    }
                    // Ehtiyat hissələri diaqnostikadan bəri "sorgu" gündür tam təmin edilməyib
                    if (r[7] is "Təminat gözlənilir" or "Qismən təmin edilib" && DateTime.TryParse(r[8] as string, out var bas) && (DateTime.Today - bas).TotalDays >= sorgu)
                        siyahi.Add(($"part:{id}", "part", "Ehtiyat hissələri gözlənilir", $"{ad}: ehtiyat hissələri {(int)(DateTime.Today - bas).TotalDays} gündür tam təmin edilməyib ({r[7]})", zv, id));
                }
            }
            if (Session.Tip != QurumTip.Zavod)
            {
                var icmal = await TexnikaRepo.ZavodIcmalAsync();
                foreach (System.Data.DataRow z in icmal.Rows)
                    if (z["SonPaket"] is long t && (DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime).TotalDays >= paket)
                        siyahi.Add(($"sync:{z["Zavod"]}:{bugun}", "sync", "Zavoddan paket gəlməyib", $"{z["Zavod"]} {(int)(DateTime.Now - DateTimeOffset.FromUnixTimeMilliseconds(t).LocalDateTime).TotalDays} gündür ixrac paketi göndərməyib", (string)z["Zavod"], 0));
            }
            // Çox olanda hər birinə ayrıca bildiriş əvəzinə bir ümumi bildiriş (siyahı reyestrdə "Gecikən" filtri ilə görünür)
            foreach (var grp in siyahi.GroupBy(x => x.nov))
            {
                if (grp.Count() > 20)
                {
                    string bas = grp.Key switch { "late" => "Təmir gecikir", "soon" => "Son tarix yaxınlaşır", "part" => "Ehtiyat hissələri gözlənilir", _ => grp.First().basliq };
                    foreach (var z in grp.GroupBy(x => x.zavod))
                        await SistemRepo.BildirisAsync($"{grp.Key}:sum:{z.Key}:{bugun}", grp.Key, bas, $"{z.Key}: {z.Count()} texnika. Siyahı üçün reyestrdə filtrdən istifadə edin.", z.Key, null);
                }
                else foreach (var b in grp) await SistemRepo.BildirisAsync(b.acar, b.nov, b.basliq, b.metn, b.zavod, b.id == 0 ? null : b.id);
            }
        }
    }
}
