using Aspose.Finance.Ofx;
using Aspose.Finance.Ofx.Bank;
using Aspose.Finance.Ofx.Signon;
using System;

namespace CSharp.WorkingWithOfxFiles
{
    class CreateOfxBankTransactionResponseFile
    {
        public static void Run()
        {
            // ExStart:1
            // Working directories
            string outputDir = RunExamples.Get_OutputDirectory();

            OfxResponseDocument document = new OfxResponseDocument();
            document.SignonResponseMessageSetV1 = new SignonResponseMessageSetV1();
            SignonResponse signonResponse = new SignonResponse();
            document.SignonResponseMessageSetV1.SignonResponse = signonResponse;
            signonResponse.Status = new Status();
            signonResponse.Status.Code = "0";
            signonResponse.Status.Severity = SeverityEnum.INFO;
            signonResponse.Status.Message = "SUCCESS";
            signonResponse.ServerDate = "20200611";
            signonResponse.ProfileUpdateDate = "20200611";
            FinancialInstitution fi = new FinancialInstitution();
            fi.Organization = "aspose";
            fi.FinancialInstitutionId = "1";
            signonResponse.FinancialInstitution = fi;
            signonResponse.SessionCookie = "11111111111111111";

            document.BankResponseMessageSetV1 = new BankResponseMessageSetV1();
            StatementTransactionResponse stmtTransResponse = new StatementTransactionResponse();
            document.BankResponseMessageSetV1.StatementTransactionResponses.Add(stmtTransResponse);
            stmtTransResponse.TransactionUniqueId = "829631324";
            stmtTransResponse.Status = new Status();
            stmtTransResponse.Status.Code = "0";
            stmtTransResponse.Status.Severity = SeverityEnum.INFO;
            stmtTransResponse.StatementResponse = new StatementResponse();
            stmtTransResponse.StatementResponse.Currency = CurrencyEnum.USD;
            stmtTransResponse.StatementResponse.BankAccountFrom = new BankAccount();
            stmtTransResponse.StatementResponse.BankAccountFrom.BankId = "1111111";
            stmtTransResponse.StatementResponse.BankAccountFrom.AccountId = "1111111111111";
            stmtTransResponse.StatementResponse.BankAccountFrom.AccountType = AccountEnum.CHECKING;
            stmtTransResponse.StatementResponse.BankTransactionList = new BankTransactionList();
            stmtTransResponse.StatementResponse.BankTransactionList.StartDate = "20200601000000";
            stmtTransResponse.StatementResponse.BankTransactionList.EndDate = "20200611000000";
            StatementTransaction transaction1 = new StatementTransaction();
            transaction1.TransactionType = TransactionEnum.DEBIT;
            transaction1.PostedDate = "20200611000000";
            transaction1.TransactionAmount = "-12";
            transaction1.FinancialInstitutionTransactionId = "1111111111111111111111111";
            transaction1.Name = "bbbbbbbbbbbbbbbbbbbbbbb";
            StatementTransaction transaction2 = new StatementTransaction();
            transaction2.TransactionType = TransactionEnum.CREDIT;
            transaction2.PostedDate = "20200611000000";
            transaction2.TransactionAmount = "22222.11";
            transaction2.FinancialInstitutionTransactionId = "2222222222222222222222222222";
            transaction2.Name = "wwwwwwwwwwwwwwwwwwwwwwww";
            stmtTransResponse.StatementResponse.BankTransactionList.StatementTransactions.Add(transaction1);
            stmtTransResponse.StatementResponse.BankTransactionList.StatementTransactions.Add(transaction2);
            stmtTransResponse.StatementResponse.LedgerBalance = new LedgerBalance();
            stmtTransResponse.StatementResponse.LedgerBalance.BalanceAmount = "+2222.42";
            stmtTransResponse.StatementResponse.LedgerBalance.DateAsOf = "20200611000000";
            stmtTransResponse.StatementResponse.AvailableBalance = new AvailableBalance();
            stmtTransResponse.StatementResponse.AvailableBalance.BalanceAmount = "+222222.42";
            stmtTransResponse.StatementResponse.AvailableBalance.DateAsOf = "20200611000000";

            document.Save(outputDir + @"newOfxResponseBankStatement.xml", OfxVersionEnum.V2x);
            document.Save(outputDir + @"newOfxResponseBankStatement.sgml", OfxVersionEnum.V1x);
            // ExEnd:1

            Console.WriteLine("CreateOfxBankTransactionResponseFile executed successfully.");
        }
    }
}
