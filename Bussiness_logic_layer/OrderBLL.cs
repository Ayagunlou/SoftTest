using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using SoftTest.Models;

namespace Bussiness_logic_layer
{
    public class OrderBLL:IOrderBLL
    {
        private readonly Data_Access_Layer.OrderDAL _DAL;
        public OrderBLL() 
        {
            _DAL = new Data_Access_Layer.OrderDAL();
        }


        public List<PreOrderModel> GetAll()
        {
            return _DAL.GetAll();
        }

        public List<PreOrderModel> GetPara()
        {
            var result = _DAL.GetPara();
            return result;
        }

        public bool Add(PreOrderModel order)
        {
            if (order == null)
            {
                return false;
            }
            order.Terminate = 'o';
            order.Total = order.Price * order.quantity;
            var result = _DAL.Add(order);
            return result;
        }

        public bool Update(PreOrderModel order)
        {
            if (order == null && order.Terminate != 'c' && order.Terminate != 'g')
            {
                return false;
            }
            order.Total = order.Price * order.quantity;
            var result = _DAL.Update(order);
            return result;
        }

        public PreOrderModel FindById(int id)
        {
            var result = _DAL.FindById(id);
            if(result == null)
            {
                throw new Exception("Id Not Find");
            }
            return result;
        }

        public bool ChangeIdentityToOpen(int id, char terminate)
        {
            if (id.Equals(null) && terminate.Equals(null))
            {
                return false;
            }
            char status = 'o';
            if(terminate == status || terminate == 'c' || terminate == 'g')
            {
                return false;
            }
            var result = _DAL.ChangeIdentity(status, id);
            return result;
        }


        public bool ChangeIdentityToApproval(int id, char terminate)
        {
            if (id.Equals(null) && terminate.Equals(null))
            {
                return false;
            }
            char status = 'a';
            if (terminate == status || terminate == 'c' || terminate == 'g')
            {
                return false;
            }
            var result = _DAL.ChangeIdentity(status, id);
            return result;
        }

        public bool ChangeIdentityToCancel(int id, char terminate)
        {
            if (id.Equals(null) && terminate.Equals(null))
            {
                return false;
            }
            char status = 'c';
            if (terminate == status)
            {
                return false;
            }
            var result = _DAL.ChangeIdentity(status, id);
            return result;
        }

        //public bool Register(UserModel user)
        //{
            
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    var result = _DAL.Register(user);
        //    return result;
        //}

        //public string Login(UserModel user)
        //{
        //    if(user == null)
        //    {
        //        return user.UserName.ToString();
        //    }
        //    var result = _DAL.Logined(user);
        //    return result;
        //}

        public bool ChangeIdentityToGen(List<int> id, char terminate)
        {
            if (id.Equals(null) && terminate.Equals(null))
            {
                return false;
            }
            char status = 'c';
            if (terminate == status)
            {
                return false;
            }
            foreach(var rid in id)
            {
                try
                {
                var result = _DAL.ChangeIdentity('g', rid);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return true;
        }
    }
}