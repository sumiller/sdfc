﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Net;
using MySql.Data.MySqlClient;
using System.Data;

namespace SDFC.Models
{
    /// <summary>
    /// Class used to CRUD Records in Database
    /// </summary>
    public class DB_Connector
    {
        private MySqlConnection myConnection;

        public DB_Connector()
        {
            //get connection string from web.config
            string connectionString = WebConfigurationManager.AppSettings["ConnectionString"];

            //create db connection
            myConnection = new MySqlConnection(connectionString);
        }

        /// <summary>
        /// Attempts to authenticate user against database
        /// </summary>
        /// <param name="userName">User's asurite ID</param>
        /// <param name="password">User's password</param>
        /// <returns></returns>
        public bool LogIn(string userName, string password)
        {
            //create command
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM employee WHERE asurite='"+userName+"' AND password='"+password+"'");          
            //create reader
            MySqlDataReader reader = null;

            //give connection to cmd
            cmd.Connection = myConnection;

            //create and initialize boolean to store query result
            bool result = false;

            try
            {
                //open connection to mySql server
                myConnection.Open();
                
                //prepare statement
                cmd.Prepare();

                //run command
                reader = cmd.ExecuteReader();

                //store whether a record was found
                result = reader.HasRows;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //dispose
                myConnection.Close();
            }
            //return query result
            return result;
        }

        public bool AddReport(AccidentReport report)
        {
            //temporary
            //create command
            MySqlCommand cmd = new MySqlCommand("fileReport");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(new MySqlParameter[] {
                new MySqlParameter("@Actions", report.Action),
                new MySqlParameter("@Activity", report.Activity),
                new MySqlParameter("@Address", report.Address),
                new MySqlParameter("@Age", report.Age),
                new MySqlParameter("@ArrivalTime", report.ArrivalTime),
                new MySqlParameter("@Comments", report.Comments),
                new MySqlParameter("@Description", report.Description),
                //new MySqlParameter("@FacMan", report.FacilitiesManagemet),
                new MySqlParameter("@InjuryLocation", report.InjuryLocation),
                new MySqlParameter("@InjuryType", report.InjuryType),
                new MySqlParameter("@Location", report.location),
                new MySqlParameter("@Male", report.Male),
                new MySqlParameter("@ManagerCalled", report.ManagerCalled),
                new MySqlParameter("@MedicalReportNo", report.MedicalReport),
                new MySqlParameter("@ASUID", report.Name),
                new MySqlParameter("@Phone", report.Phone),
                new MySqlParameter("@PoliceContacted", report.PoliceContacted),
                new MySqlParameter("@PositionTitles", report.PositionTitles),
                new MySqlParameter("@ReportNo", report.ReportNumber),
                new MySqlParameter("@VictimSignature", report.SignatureJSON),
                new MySqlParameter("@TimeCalled", report.TimeCalled),
                new MySqlParameter("@TransportedTo", report.TransportedTo),
                new MySqlParameter("@Treatment", report.Treatment),
                new MySqlParameter("@Treator", report.Treator),
                new MySqlParameter("@VictimName", report.VictimName),
                new MySqlParameter("@WhyNot", report.WhyNot),
                new MySqlParameter("@WitnessName", report.WitnessName),
                new MySqlParameter("@WitnessPhone", report.WitnessPhone),
                new MySqlParameter("@RefusalSignature", null),
                }
            );
            //create reader
            MySqlDataReader reader = null;

            //give connection to cmd
            cmd.Connection = myConnection;

            //create and initialize boolean to store query result
            bool result = false;

            try
            {
                //open connection to mySql server
                myConnection.Open();

                //prepare statement
                cmd.Prepare();

                //run command
                reader = cmd.ExecuteReader();

                //store whether a record was found
                result = reader.HasRows;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                //dispose
                myConnection.Close();
            }
            
            //return query result            
            return true;
        }
    }
}