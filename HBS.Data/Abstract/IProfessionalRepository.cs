using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HBS.Entities;

namespace HBS.Data.Abstract
{
    public interface IProfessionalRepository
    {
        int AddProfessional(Professional professional);
        bool UpdateProfessional(Professional professional);
        Professional GetProfessional(int professionalId);

        List<Professional> GetProfessionals(int companyId, string professionalName);
        bool RemoveProfessional(int professionalId, int removedBy);//
        bool AddProfessionalSchedule(KendoEntity professionalSchedule);
        bool UpdateProfessionalSchedule(KendoEntity professionalSchedule);
        ProfessionalSchedule GetProfessionalSchedule(int professionalSchedulreId);
        List<ProfessionalSchedule> GetProfessionalScheduleListByScheduleId(int scheduleId);
        List<ProfessionalSchedule> GetProfessionalScheduleByScheduleDate(DateTime scheduleDate);
        bool RemoveProfessionalSchedule(int professionalSchduleId, int removedBy);//

        List<KendoEntity> GetProfessionalMonthlyAppointments(int professionalId, int Month, int Year);

    }
}
