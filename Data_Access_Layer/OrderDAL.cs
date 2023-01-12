using Data_Access_Layer.Repository.Models;
using Microsoft.EntityFrameworkCore;
using SoftTest.Data;
using SoftTest.Models;


namespace Data_Access_Layer
{
    public class OrderDAL
    {
        private readonly DataContext _db;

        public OrderDAL()
        {
            _db = new DataContext();
        }
        public List<PreOrderModel> GetAll()
        {
            return _db.preOrders.ToList();
        }

        public List<PreOrderModel> GetPara()
        {
            //var sc = _db.preOrders.Where(c => c.Terminate == 'a').GroupBy(c => c.Vender, c => c.Vender, (key, g) => new { x1 = key, x2 = g.ToList() });
            //var ep = _db.preOrders.Where(c => c.Terminate == 'a').GroupBy(c => c.Vender, c => c.Vender).ToList();
            //var lep = ep.Select(ep => ep.Key).ToList();
            //var sc = _db.preOrders.Where(c => c.Terminate == 'a').ToList();
            //var sc = _db.preOrders.Where(c => c.Terminate == 'a').FirstOrDefault(c=>c.Vender==c.Vender);
            //return _db.preOrders.Where(c=>c.Terminate=='a').Where(x=>x.Vender==sc.Vender).ToList();
            return _db.preOrders.Where(c => c.Terminate == 'a').ToList();
        }

        public bool Add(PreOrderModel order)
        {
            try
            {
                _db.preOrders.Add(order);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;

        }

        public bool Update(PreOrderModel order)
        {
            try
            {
                _db.preOrders.Update(order);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        public PreOrderModel FindById(int id)
        {
            PreOrderModel po = new PreOrderModel();
            try
            {
                po = _db.preOrders.FirstOrDefault(c => c.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return po;
        }

        public bool ChangeIdentity(char Identity, int id)
        {
            try
            {
                _db.preOrders.Where(t => t.Id == id)
                    .ExecuteUpdate(s => s
                    .SetProperty(b=>b.Terminate, Identity));
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
            return true;
        }

        //public bool Register(UserModel user)
        //{
        //    try
        //    {
        //    _db.UserModels.Add(user);
        //    _db.SaveChanges();
        //    }catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return false;
        //    }
        //    return true;
        //}

        //public string Logined(UserModel user) 
        //{
        //    try
        //    {
        //       var result = _db.UserModels.FirstOrDefault(c=>c.UserName== user.UserName.ToLower());
        //       return result.Id.ToString();
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //        return user.UserName.ToString().ToLower();
        //    }
        //}

    }
}