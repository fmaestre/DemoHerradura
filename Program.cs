using System;
using System.Linq;
using Herradura.Lib.Portal.DAL;
using System.Data;
using Herradura.Lib.core;
using System.Collections;
using System.Collections.Generic;

namespace DemoHerradura
{
    public class Program
    {
        static void Main(string[] args)
        {
            //testing a select.
            DOSelect();
            Console.ReadLine();
            //testing an update.
            BL.updateInsurances(new DemoherraduraComp { Insurancename = "C", Address = "Tecate" });
            DOSelect();
            Console.WriteLine("Item was updated");
           Console.ReadLine();
            //testing a delete.
            BL.deleteInsurances(new DemoherraduraComp { Insurancename = "C"});
            DOSelect();
            Console.WriteLine("Item was deleted");
            Console.ReadLine();
            //testing an insert.
            BL.insertInsurances(new DemoherraduraComp { Insurancename = "C", Address = "NEW C" });            
            DOSelect();
            Console.WriteLine("Item was inserted");
            Console.ReadLine();
        }

        static void DOSelect()
        {
            var l =  BL.getInsurances(new DemoherraduraComp { Insurancename = "" });
            foreach (var i in l) {
                Console.WriteLine(string.Format("Test completed successfully {0}-{1}", i.Insurancename, i.Address));
            }
        }



    }


    public static class BL
    {
        public static List<DemoherraduraComp> getInsurances(DemoherraduraComp comp)
        {            
            comp.Insurancename = comp.Insurancename.add_like(); 
            var x = PortalData.GenericDAL.getList(comp);
            return x.OrderBy(z => z.Insurancename).ToList();
        }
        public static void updateInsurances(DemoherraduraComp comp)
        {

            PortalData.GenericDAL.updateItem(comp);            
        }
        public static void deleteInsurances(DemoherraduraComp comp)
        {

            PortalData.GenericDAL.DeleteItemWithPK(comp);
        }
        public static void insertInsurances(DemoherraduraComp comp)
        {
      
            PortalData.GenericDAL.insertItem(comp);
        }

    }



    public static class PortalData
    {
        public static GenericDALSQLProvider GenericDAL { get { return new GenericDALSQLProvider("CNR"); } }

    }

    [Serializable()]
    [Table("DemoHerradura")]
    public partial class DemoherraduraComp : BaseComponentClass, ICatalog
    {
        #region Class Instance Variables
        private string _insurancename = string.Empty;
        private string _address = string.Empty;
        #endregion
        #region Contructors
        public DemoherraduraComp() : base()
        {
        }
        #endregion
        #region Public Interface
        #region properties
        [Field("insuranceName", "Insurancename", true, enmDataTypes.stringType, true, true)]
        public string Insurancename
        {
            get { return _insurancename; }
            set
            {
                _insurancename = value;
                this.firePropertyChange("Insurancename");
            }
        }
        [Field("Address", "Address", false, enmDataTypes.stringType, true, true)]
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                this.firePropertyChange("Address");
            }
        }
        public override void resetObjects()
        {
            _insurancename = string.Empty;
            _address = string.Empty;
             base.resetObjects();
        }
        #endregion
        #region Methods
        #endregion //Termina Metodos
        #region ICatalog Members
        ArrayList ICatalog.getPropertyChanges()
        {
            return base._propChanges;
        }
        void ICatalog.markAsSaved()
        {
            base.markCompAsSaved();
        }
        public void markAsUnSaved()
        {
            base.MarkCompAsUnSaved();
        }
        #endregion
        #endregion //termina public interface
        #region Private Interface
        #endregion //Termina Private Interface
    }

}
