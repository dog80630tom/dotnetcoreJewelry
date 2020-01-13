using Jewelry.Models;
using Jewelry.Respository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jewelry.Services
{
    public class MemberServices
    {
        private static DataContext context;
        private CRUDRespository<Member> MemberRespository=new CRUDRespository<Member>(context);
        public bool CreateMember(Member member) {

            try
            {

                MemberRespository.Crate(member);

                MemberRespository.SaveChange();
            }
            catch (Exception ex)
            {
               return false;
            }
            return true;

        }
        public bool EditMember(Member member)
        {

            try
            {

                MemberRespository.Update(member);

                MemberRespository.SaveChange();
            }
            catch (Exception ex)
            {


                return false;
            }
            return true;

        }
        public bool DeleteMember(Member member)
        {

            try
            {

                MemberRespository.Delete(member);

                MemberRespository.SaveChange();
            }
            catch (Exception ex)
            {


                return false;
            }
            return true;

        }
    }
}
