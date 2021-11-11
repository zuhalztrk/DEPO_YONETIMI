using DEPO_YONETIMI.Module.BusinessObjects.HAREKET;
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

namespace DEPO_YONETIMI.Module.BusinessObjects.TANIMLAR
{
    [DefaultClassOptions]
    [DefaultProperty("Ad")]
    [NavigationItem("Tanımlar")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class TANIMLAR_StokKarti : TANIMLAR_Kart
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public TANIMLAR_StokKarti(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }
        private byte[] _stok_resim;
        [ImageEditor]
        public byte[] StokResmi
        {
            get { return _stok_resim; }
            set { SetPropertyValue(nameof(StokResmi), ref _stok_resim, value); }
        }

        private string _musteri_telefon;
        public string MusteriTelefon
        {
            get { return _musteri_telefon; }
            set { SetPropertyValue(nameof(MusteriTelefon), ref _musteri_telefon, value); }
        }
        [Association("Stok-Fonksiyonu")]
        public XPCollection<HAREKET_StokHareket> Depolar
        {
            get
            {
                return GetCollection<HAREKET_StokHareket>(nameof(Depolar));
            }
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
    }
}