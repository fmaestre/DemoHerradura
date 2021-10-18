using System;
using System.Linq;
using Herradura.Lib.Portal.DAL;
using System.Data;
using Herradura.Lib.core;
using System.Collections;

namespace DemoHerradura
{
    public class Program
    {
        static void Main(string[] args)
        {
            var l = new Program().getInsurances(new DemoherraduraComp { Insurancename = "" });

            Console.WriteLine("Test completed successfully {0}",l.Count);
            Console.ReadLine();
        }
        public System.Collections.Generic.List<DemoherraduraComp> getInsurances(DemoherraduraComp comp)
        {            
            comp.Insurancename = comp.Insurancename.add_like(); 
            var x = PortalData.GenericDAL.getList(comp);
            return x.OrderBy(z => z.Insurancename).ToList();
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
