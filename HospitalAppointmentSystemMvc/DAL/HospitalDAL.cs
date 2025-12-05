using HospitalAppointmentSystem.Models;
using HospitalAppointmentSystemMvc.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace HospitalAppointmentSystemMvc.DAL
{
    public class HospitalDAL
    {
        private readonly string Connection = ConfigurationManager.ConnectionStrings["HospitalDB"].ConnectionString;

        // Get all doctors (calls sp_GetDoctors)
        public List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetDoctors", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            doctors.Add(new Doctor()
                            {
                                DoctorID = Convert.ToInt32(reader["DoctorID"]),
                                Name = reader["Name"].ToString(),
                                Specialization = reader["Specialization"].ToString(),
                            });
                        }
                    }
                }
            }
            return doctors;
        }

        // Register patient (calls sp_RegisterPatient)
        public bool RegisterPatient(Patient patient)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RegisterPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", patient.Name);
                    cmd.Parameters.AddWithValue("@Email", patient.Email);
                    cmd.Parameters.AddWithValue("@Password", patient.Password);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        // propagate friendly message
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        // Login patient (calls sp_LoginPatient)
        public Patient Login(string email, string password)
        {
            Patient patient = null;
            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_LoginPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            patient = new Patient
                            {
                                PatientID = Convert.ToInt32(reader["PatientID"]),
                                Name = reader["Name"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                        }
                    }
                }
            }
            return patient;
        }

        // Book appointment (calls sp_BookAppointment)
        public bool BookAppointment(Appointment appointment)
        {
            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_BookAppointment", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", appointment.PatientID);
                    cmd.Parameters.AddWithValue("@DoctorID", appointment.DoctorID);
                    // send as DATE and NVARCHAR time
                    cmd.Parameters.AddWithValue("@AppointmentDate", appointment.AppointmentDate);
                    cmd.Parameters.AddWithValue("@AppointmentTime", appointment.AppointmentTime);
                    con.Open();
                    try
                    {
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }

        // Get appointments for patient (calls sp_GetAppointmentsByPatient)
        public List<Appointment> GetAppointments(int patientId)
        {
            List<Appointment> list = new List<Appointment>();
            using (SqlConnection con = new SqlConnection(Connection))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetAppointmentsByPatient", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", patientId);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Appointment()
                            {
                                AppointmentID = Convert.ToInt32(reader["AppointmentID"]),
                                DoctorName = reader["DoctorName"].ToString(),
                                Specialization = reader["Specialization"].ToString(),
                                AppointmentDate = Convert.ToDateTime(reader["AppointmentDate"]).ToString("yyyy-MM-dd"),
                                AppointmentTime = reader["AppointmentTime"].ToString(),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return list;
        }
    }
}
