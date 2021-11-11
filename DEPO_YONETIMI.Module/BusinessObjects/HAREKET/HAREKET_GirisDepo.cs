using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
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
    [Appearance("Device-Type-2-Visibility", Visibility = ViewItemVisibility.Hide, TargetItems = "CikisDepo")]
    [NavigationItem("Hareketler")]

    //[Appearance("HAREKET_StokHareket3", AppearanceItemType = "ViewItem", TargetItems = "*", Criteria = "Bakiye<=50", Context = "ListView", BackColor = "red", FontColor = "white", Priority = 1)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class HAREKET_GirisDepo : HAREKET_DepoTransferi
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public HAREKET_GirisDepo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (propertyName == nameof(Miktar) && GirisDepo != null && Stok != null)
            {
                var girisDepoStok = Session.Query<HAREKET_StokHareket>().Where(i => i.StokKarti.Kod == Stok.Kod && i.DepoKarti.Kod == GirisDepo.Kod).FirstOrDefault();


                if (girisDepoStok != null)
                {
                    girisDepoStok.Bakiye += Miktar;

                }

                if (girisDepoStok == null)
                {
                    var stokHareket = new HAREKET_StokHareket(Session)
                    {
                        DepoKarti = GirisDepo,
                        StokKarti = Stok,
                        Bakiye = Miktar
                    };

                    Session.Save(stokHareket);
                    girisDepoStok.Bakiye += Miktar;
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
}