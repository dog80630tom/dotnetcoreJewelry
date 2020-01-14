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
        private readonly ICRUD<Member> _MemberCRUD;
        public MemberServices(ICRUD<Member> cRUD) {

            _MemberCRUD = cRUD;
        }
        public bool CreateMember(Member member) {
            bool OK = false;
            _MemberCRUD.Crate(member);
            _MemberCRUD.SaveChange();
            OK = true;
            return OK;

        }
        public bool EditMember(Member member) {
            bool OK = false;
            _MemberCRUD.Update(member);
            _MemberCRUD.SaveChange();
            OK = true;
            return OK;

        }

        public bool DeleteMember(Member member)
        {
            bool OK = false;
            _MemberCRUD.Delete(member);
            _MemberCRUD.SaveChange();
            OK = true;
            return OK;

        }
    }
}
