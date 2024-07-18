using College.Models;
using System;
using System.Data.SqlClient;

namespace College.services
{
    internal static class PaymentService
    {
        public static void AddPayment(int enrollmentId, decimal amount, DateTime paymentDate)
        {
            // Assuming you have a DatabaseHelper class to handle database operations
            DatabaseHelper dbHelper = DatabaseHelper.Instance();

            // SQL query to insert payment into Payments table
            string query = @"
                INSERT INTO Payments (EnrollmentId, Amount, PaymentDate)
                VALUES (@EnrollmentId, @Amount, @PaymentDate)";

            // Parameters for SQL query
            SqlParameter[] parameters = {
                new SqlParameter("@EnrollmentId", enrollmentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@PaymentDate", paymentDate)
            };

            // Execute the query
            dbHelper.ExecuteNonQuery(query, parameters);
        }

        // Add other payment-related methods as needed
    }
}
