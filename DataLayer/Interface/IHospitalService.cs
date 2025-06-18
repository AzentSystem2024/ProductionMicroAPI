using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroApi.Models; 

namespace MicroApi.DataLayer.Interface
{
   
        public interface IHospitalService
        {
            HospitalResponse Insert(Hospital hospital);
            HospitalResponse Update(HospitalUpdate hospital);
            HospitalResponse GetHospitalById(int id);
            HospitalListResponse GetLogList(int? id = null);
            HospitalResponse DeleteHospitalData(int id);
        }


}
    