using DEPO_YONETIMI.Module.BusinessObjects.TANIMLAR;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
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
    [Appearance("HAREKET_StokHareket", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Bakiye<150 && Bakiye>50", Context = "ListView", BackColor = "yellow", FontColor = "Maroon", Priority = 2)]
    [Appearance("HAREKET_StokHareket2", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Bakiye<=50", Context = "ListView", BackColor = "red", FontColor = "white", Priority = 1)]
    [Appearance("HAREKET_StokHareket3", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Bakiye>=150", Context = "ListView", BackColor = "green", FontColor = "white", Priority = 3)]
    [NavigationItem("Hareketler")]

    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HAREKET_StokHareket : BaseObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HAREKET_StokHareket(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }





        private TANIMLAR_DepoKarti _depo_karti;
        [Association("Depo-Fonksiyonu")]
        public TANIMLAR_DepoKarti DepoKarti
        {
            get
            {
                return _depo_karti;
            }
            set
            {
                SetPropertyValue(nameof(DepoKarti), ref _depo_karti, value);
            }
        }

        private TANIMLAR_StokKarti _stok_karti;
        [Association("Stok-Fonksiyonu")]
        public TANIMLAR_StokKarti StokKarti
        {
            get
            {
                return _stok_karti;
            }
            set
            {
                SetPropertyValue(nameof(StokKarti), ref _stok_karti, value);
            }

        }


        private int _bakiye;
        public int Bakiye
        {
            get
            {
                return _bakiye;
            }
            set
            {
                SetPropertyValue(nameof(Bakiye), ref _bakiye, value);
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