using Project2_Server.Model;

namespace Project2_Server.API
{
    public class DTO_OrderProject
    {
        // FIELDS
        public DMODEL_Order DMODEL_Order { get; set; }
        public List<DMODEL_Project> LIST_DMODEL_Projects { get; set; }

        // CONSTRUCTORS
        public DTO_OrderProject() { }
        public DTO_OrderProject(DMODEL_Order INPUT_DMODEL_Order, List<DMODEL_Project> INPUT_LIST_DMODEL_Projects)
        {
            this.DMODEL_Order = INPUT_DMODEL_Order;
            this.LIST_DMODEL_Projects = INPUT_LIST_DMODEL_Projects;
        }

        // METHODS

        public void DTO_OrderProject_verifyData()
        {
            this.DMODEL_Order.DMODEL_ORDER_verifyData();
            foreach(DMODEL_Project TEMP_Project in LIST_DMODEL_Projects)
            {
                TEMP_Project.DMODEL_PROJECT_verifyData();
            }
        }
    }
}
