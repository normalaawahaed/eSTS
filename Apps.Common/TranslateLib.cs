using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Common
{
    public class TranslateLib
    {
        public void MenuBM(ref string menuText)
        {
            switch (menuText)
            {
                case "System Settings":
                    menuText = "Tetapan Sistem";
                    break;
                case "Dashboard":
                    menuText = "Papan Pemuka";
                    break;
                case "Master Setup":
                    menuText = "Persediaan Induk";
                    break;
                case "Establishment":
                    menuText = "Perjawatan";
                    break;
                case "Staff Information":
                    menuText = "Pengurusan Pekerja";
                    break;
                case "Service Management":
                    menuText = "Pengurusan Perkhidmatan";
                    break;
                case "Leave Management":
                    menuText = "Pengurusan Cuti";
                    break;
                case "Personal Information":
                    menuText = "Maklumat Peribadi";
                    break;
                case "System User Group":
                    menuText = "Kumpulan Pengguna Sistem";
                    break;
                case "System User Level":
                    menuText = "Level Pengguna Sistem";
                    break;
                case "Access Module":
                    menuText = "Modul Akses";
                    break;
                case "User Group Module":
                    menuText = "Module Kumpulan Pengguna";
                    break;
                case "Department Setup":
                    menuText = "Persediaan Bahagian";
                    break;
                case "Unit Setup":
                    menuText = "Persediaan Unit";
                    break;
                case "Work Base Setup":
                    menuText = "Persediaan Asas Kerja";
                    break;
                case "Country Setup":
                    menuText = "Persediaan Negara";
                    break;
                case "State Setup":
                    menuText = "Persediaan Negeri";
                    break;
                case "City Setup":
                    menuText = "Persediaan Bandar";
                    break;
                case "Religion Setup":
                    menuText = "Persediaan Agama";
                    break;
                case "Bank Setup":
                    menuText = "Persediaan Bank";
                    break;
                case "Attachment Type Setup":
                    menuText = "Persediaan Jenis Lampiran";
                    break;
                case "Setup":
                    menuText = "Persediaan";
                    break;
                case "Grade":
                    menuText = "Gred";
                    break;
                case "Position":
                    menuText = "Jawatan";
                    break;
                case "Position Title":
                    menuText = "Tajuk Jawatan";
                    break;
                case "Allowance Type":
                    menuText = "Jenis Elaun";
                    break;
                case "Service Group":
                    menuText = "Kumpulan Perkhidmatan";
                    break;
                case "Scheme":
                    menuText = "Skim";
                    break;
                case "Salary":
                    menuText = "Gaji";
                    break;
                case "Allowance":
                    menuText = "Elaun";
                    break;
                case "Others Allowance":
                    menuText = "Elaun Lain-Lain";
                    break;
                case "Warrant":
                    menuText = "Waran";
                    break;
                case "Establishment Data":
                    menuText = "Data Perjawatan";
                    break;
                case "List With Position":
                    menuText = "Senarai Jawatan";
                    break;
                case "Bank Account Type":
                    menuText = "Jenis Akaun Bank";
                    break;
                case "Bank Account Purpose":
                    menuText = "Tujuan Akaun Bank";
                    break;
                case "Driving License Type":
                    menuText = "Jenis Lesen Memandu";
                    break;
                case "Driving License Class":
                    menuText = "Kelas Lesen Memandu";
                    break;
                case "Relationship":
                    menuText = "Perhubungan";
                    break;
                case "Liability Code":
                    menuText = "Kod Tanggungan";
                    break;
                case "Blood Type":
                    menuText = "Jenis Darah";
                    break;
                case "Education Level":
                    menuText = "Level Pengajian";
                    break;
                case "Disability Category":
                    menuText = "Kategori Kecacatan";
                    break;
                case "Disability Cause":
                    menuText = "Punca Kecacatan";
                    break;
                case "Disease Type":
                    menuText = "Jenis Penyakit";
                    break;
                case "Institution":
                    menuText = "Institusi";
                    break;
                case "Appointed Authority":
                    menuText = "Jawatankuasa Melantik";
                    break;
                case "Appointment Status":
                    menuText = "Status Lantikan";
                    break;

            }
        }
    }
}
