using DEPO_YONETIMI.Module.BusinessObjects.TANIMLAR;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DEPO_YONETIMI.Module.BusinessObjects.HAREKET
{
    [DefaultClassOptions]
    [NavigationItem("Hareketler")]

    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HAREKET_DepoTransferi : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HAREKET_DepoTransferi(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        
        
       
        


        private DateTime _tarih;
        public DateTime Tarih
        {
            get
            {
                return _tarih;
            }
            set
            {
                SetPropertyValue(nameof(Tarih), ref _tarih, value);
            }
        }

        private TANIMLAR_DepoKarti _giren_depo;
        public TANIMLAR_DepoKarti GirisDepo
        {
            get
            {
                return _giren_depo;
            }
            set
            {
                SetPropertyValue(nameof(GirisDepo), ref _giren_depo, value);
            }
        }

        private TANIMLAR_DepoKarti _cikan_depo;

        public TANIMLAR_DepoKarti CikisDepo
        {
            get
            {
                return _cikan_depo;
            }
            set
            {
                SetPropertyValue(nameof(CikisDepo), ref _cikan_depo, value);
            }
        }

        private TANIMLAR_StokKarti _stok;
        public TANIMLAR_StokKarti Stok
        {
            get
            {
                return _stok;
            }
            set
            {
                SetPropertyValue(nameof(Stok), ref _stok, value);
            }
        }

        private int _miktar;
        public int Miktar
        {
            get
            {
                return _miktar;
            }
            set
            {
                SetPropertyValue(nameof(Miktar), ref _miktar, value);
            }
        }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if(propertyName==nameof(Miktar) && CikisDepo!=null && GirisDepo!=null && Stok!=null)
             {
                 var girisDepoStok = Session.Query<HAREKET_StokHareket>().Where(i => i.StokKarti.Kod == Stok.Kod  && i.DepoKarti.Kod == GirisDepo.Kod).FirstOrDefault();
                 var cikisDepoStok = Session.Query<HAREKET_StokHareket>().Where(i => i.StokKarti.Kod == Stok.Kod && i.DepoKarti.Kod == CikisDepo.Kod).FirstOrDefault();

                if(girisDepoStok!=null && cikisDepoStok != null)
                {
                    girisDepoStok.Bakiye += Miktar;
                    cikisDepoStok.Bakiye -= Miktar;
                }

                if (girisDepoStok == null && cikisDepoStok != null)
                {
                    var stokHareket = new HAREKET_StokHareket(Session)
                    {
                        DepoKarti = GirisDepo,
                        StokKarti = Stok,
                        Bakiye = Miktar
                    };

                    Session.Save(stokHareket);
                    cikisDepoStok.Bakiye -= Miktar;
                }
                if (girisDepoStok != null && cikisDepoStok == null)
                {
                    var stokHareket2 = new HAREKET_StokHareket(Session)
                    {
                        DepoKarti = GirisDepo,
                        StokKarti = Stok,
                        Bakiye = Miktar
                    };

                    Session.Save(stokHareket2);
                    girisDepoStok.Bakiye += Miktar;
                }

            }

            //if (propertyName == nameof(Miktar) && CikisDepo != null && GirisDepo != null && Stok != null)
            //{
            //    if (GirisDepo.Stoklar.Where(i => i.StokKarti.Kod == Stok.Kod && i.DepoKarti.Kod == GirisDepo.Kod).Any()
            //        && CikisDepo.Stoklar.Where(i => i.StokKarti.Kod == Stok.Kod && i.DepoKarti.Kod == CikisDepo.Kod).Any())
            //    {
            //        GirisDepo.Stoklar.FirstOrDefault(i => i.StokKarti.Kod == Stok.Kod && i.DepoKarti.Kod == GirisDepo.Kod).Bakiye += Miktar;
            //        CikisDepo.Stoklar.FirstOrDefault(i => i.StokKarti.Kod == Stok.Kod && i.DepoKarti.Kod == CikisDepo.Kod).Bakiye -= Miktar;
            //    }

            //    // CikanDepo.Stoklar.Where(i=>i.Kod==Stok.Kod).Any()
            //}
        }



        //private string _PersistentProperty;
        //[XafDisplayName("My display name"), ToolTip("My hint message")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        //[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
        //public string PersistentProperty {
        //    get { return _PersistentProperty; }
        //    set { SetPropertyValue(nameof(PersistentProperty), ref _PersistentProperty, value); }
        //}

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (propertyName == "GirenDepoID")
        //    {
        //        StokID.StokMiktari = StokID.StokMiktari - DepoMiktari;

        //    }
        //    if (propertyName == "CikisDepoID")
        //    {
        //        StokID.StokMiktari = StokID.StokMiktari + DepoMiktari;

        //    }
        //}

    }
}