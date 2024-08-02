using College.Models;
using College.Services;
using System;
using System.Data.SqlClient;

namespace College.services
{
    internal static class PaymentService
    {

        //add payment on specific course 

        public static void AddPayment(int enrollmentId, decimal amount, DateTime paymentDate)
        {
            // Get the current balance for the enrollment
            decimal balanceEnroll = EnrollmentService.GetEnrollmentById(enrollmentId).Balance;
            // Get the total amount paid so far
            decimal totalPaid = GetTotalPaid(enrollmentId);

            // Check if the new payment will exceed the balance
            if (amount + totalPaid > balanceEnroll)
            {
                throw new InvalidOperationException("Payment exceeds the remaining balance.");
            }

            // Insert the payment record into the Payments table
            InsertPayment(enrollmentId, amount, paymentDate);

            // Update the balance for the enrollment
            UpdateBalance(enrollmentId, balanceEnroll - (totalPaid + amount));
        }

        private static decimal GetTotalPaid(int enrollmentId)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                SELECT SUM(Amount)
                FROM Payments
                WHERE EnrollmentId = @EnrollmentId";

            SqlParameter[] parameters = {
                new SqlParameter("@EnrollmentId", enrollmentId)
            };

            object result = dbHelper.ExecuteScalar(query, parameters);
            return result != DBNull.Value ? (decimal)result : 0;
        }

        private static void InsertPayment(int enrollmentId, decimal amount, DateTime paymentDate)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                INSERT INTO Payments (EnrollmentId, Amount, PaymentDate)
                VALUES (@EnrollmentId, @Amount, @PaymentDate)";

            SqlParameter[] parameters = {
                new SqlParameter("@EnrollmentId", enrollmentId),
                new SqlParameter("@Amount", amount),
                new SqlParameter("@PaymentDate", paymentDate)
            };

            dbHelper.ExecuteNonQuery(query, parameters);
        }

        private static void UpdateBalance(int enrollmentId, decimal newBalance)
        {
            DatabaseHelper dbHelper = DatabaseHelper.Instance();
            string query = @"
                UPDATE Enrollments
                SET Balance = @NewBalance
                WHERE EnrollmentId = @EnrollmentId";

            SqlParameter[] parameters = {
                new SqlParameter("@NewBalance", newBalance),
                new SqlParameter("@EnrollmentId", enrollmentId)
            };
            dbHelper.ExecuteNonQuery(query, parameters);
        }
    }
}