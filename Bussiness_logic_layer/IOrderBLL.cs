using Data_Access_Layer.Repository.Models;
using SoftTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_logic_layer
{
    internal interface IOrderBLL
    {
        public List<PreOrderModel> GetAll();
        public List<PreOrderModel> GetPara();
        public bool Add(PreOrderModel order);
        public bool Update(PreOrderModel order);
        public PreOrderModel FindById(int id);
        public bool ChangeIdentityToOpen(int id, char terminate);
        public bool ChangeIdentityToApproval(int id, char terminate);
        public bool ChangeIdentityToCancel(int id, char terminate);
        public bool ChangeIdentityToGen(List<int> id, char terminate);
        //public bool Register(UserModel user);
        //public string Login (UserModel user);
    }
}
