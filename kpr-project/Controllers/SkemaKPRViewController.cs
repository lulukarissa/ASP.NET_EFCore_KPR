using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kpr_project.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kpr_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //public class SkemaKPRViewController : ControllerBase
    public class SkemaKPRViewController : Controller
    {
        private readonly kprContext _Context;

        public SkemaKPRViewController(kprContext context)
        {
            _Context = context;
        }
        
        [HttpGet]
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<SkemaKpr>>> skemaKPRDetail()
        {
            var List = await _Context.SkemaKpr.ToListAsync();
            return List;
        }

        //calculateSkemaKPR
        [HttpGet("{IDSkema}")]
        //[HttpGet]
        public async Task<ActionResult<IEnumerable<SkemaDetail>>> skemaKPRDetail(Guid IDSkema)
        {
            var List = await _Context.SkemaDetail.Where(s => s.Idskema == IDSkema).OrderBy(s => s.Bulan).ToListAsync();
            return List;
        }
        
        [HttpPost]
        //public async Task<ActionResult<SkemaDetail>> calculateSkemaKPR(decimal harga, decimal dp, decimal bunga, int tenor)
        //public async Task<ActionResult<Guid>> calculateSkemaKPR(SkemaKPR inputForm)
        public async Task<ActionResult<SkemaKpr>> calculateSkemaKPR(SkemaKpr inputForm)
        {
            var input = new SkemaKpr
            {
                Harga = inputForm.Harga,
                Dp = inputForm.Dp,
                Bunga = inputForm.Bunga,
                Tenor = inputForm.Tenor
            };

            _Context.SkemaKpr.Add(input);
            await _Context.SaveChangesAsync();

            //Menghitung Uang Muka
            decimal SisaKredit = (inputForm.Harga - (inputForm.Harga * (inputForm.Dp / 100)));
            //perhitungan tagihan tahapan angsuran tetap
            decimal Tagihan = 0;
            //menghitung besaran bunga perbulan
            decimal BungaPerBulan = (inputForm.Bunga / 12) / 100;
            //menhitung total tahapan angsuran dalam bulan
            int TahapanAngsuran = inputForm.Tenor * 12;
            //menghitung sisa pokok ditambah bunga per bulan
            decimal CalculateDPPlusBunga = SisaKredit * BungaPerBulan;
            //menghitung (1 + bunga perbulan)
            decimal CalculateOnePlusBunga = 1 + BungaPerBulan;
            //menghitung pangkat atas (1 + buanga perbulan) dipangkat total tahapan angsuran 
            double CalculatePow1 = Math.Pow((double)CalculateOnePlusBunga, TahapanAngsuran);
            //menghitung pangkat bawah (1 + buanga perbulan) dipangkat total tahapan angsuran dikurang 1
            double CalculatePow2 = CalculatePow1 - 1;
            //menghitung pemangkatan atas dibagi pemangkatan bawah
            double CalculateX = CalculatePow1 / CalculatePow2;
            //menghitung tagihan angsuran tetap
            Tagihan = CalculateDPPlusBunga * (decimal)CalculateX;
            //declarasi sisa kredit
            decimal PokokKredit = SisaKredit;

            for (int i = 1; i <= TahapanAngsuran; i++)
            {
                decimal Bungakredit = PokokKredit * BungaPerBulan;
                decimal TagihanPokok = Tagihan - Bungakredit;
                decimal SisaTagihanPokok = PokokKredit - TagihanPokok;
                
                var inputanKPRDetail = new SkemaDetail
                {
                    Bulan = i,
                    Pokok = PokokKredit,
                    Bunga = Bungakredit,
                    Pelunasanpokok = TagihanPokok,
                    Tagihan = Tagihan,
                    Sisapokok = SisaTagihanPokok
                };
                inputanKPRDetail.Idskema = input.Idskema;

                _Context.SkemaDetail.Add(inputanKPRDetail);
                
                PokokKredit = SisaTagihanPokok;
                await _Context.SaveChangesAsync();
            }
            //parameter =  new { IDSkemaTest = input.IDSkema };
            var inputResult = new SkemaKpr
            {
                Idskema = input.Idskema,
                Harga = inputForm.Harga,
                Dp = inputForm.Dp,
                Bunga = inputForm.Bunga,
                Tenor = inputForm.Tenor
            };
            //var test = new SkemaKPR { IDSkema = input.IDSkema };
            return inputResult;
        }
    }
}