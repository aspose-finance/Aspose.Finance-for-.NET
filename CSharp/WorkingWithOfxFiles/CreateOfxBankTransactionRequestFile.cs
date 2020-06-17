using Aspose.Finance.Ofx;
using Aspose.Finance.Ofx.Bank;
using Aspose.Finance.Ofx.Signon;
using System;

namespace CSharp.WorkingWithOfxFiles
{
    class CreateOfxBankTransactionRequestFile
    {
        public static void Run()
        {
            // ExStart:1
            // Working directories
            string outputDir = RunExamples.Get_OutputDirectory();

            OfxRequestDocument document = new OfxRequestDocument();
            document.SignonRequestMessageSetV1 = new SignonRequestMessageSetV1();
            SignonRequest signonRequest = new SignonRequest();
            document.SignonRequestMessageSetV1.SignonRequest = signonRequest;
            signonRequest.ClientDate = "20200611000000";
            signonRequest.UserId = "aspose";
            signonRequest.UserPassword = "password";
            FinancialInstitution fi = new FinancialInstitution();
            fi.Organization = "aspose";
            fi.FinancialInstitutionId = "1";
            signonRequest.FinancialInstitution = fi;
            signonRequest.AppVersion = "1.0";
            signonRequest.AppId = "Aspose.Finance";
            signonRequest.ClientUserId = "aaaaaaa";

            document.BankRequestMessageSetV1 = new BankRequestMessageSetV1();
            StatementTransactionRequest stmtTransRequest = new StatementTransactionRequest();
            document.BankRequestMessageSetV1.StatementTransactionRequests.Add(stmtTransRequest);
            stmtTransRequest.TransactionUniqueId = "1111111";
            stmtTransRequest.StatementRequest = new StatementRequest();
            stmtTransRequest.StatementRequest.BankAccountFrom = new BankAccount();
            stmtTransRequest.StatementRequest.BankAccountFrom.BankId = "sssss";
            stmtTransRequest.StatementRequest.BankAccountFrom.AccountId = "sfsdfsfsdf";
            stmtTransRequest.StatementRequest.BankAccountFrom.AccountType = AccountEnum.CHECKING;
            stmtTransRequest.StatementRequest.IncTransaction = new IncTransaction();
            stmtTransRequest.StatementRequest.IncTransaction.StartDate = "20200601000000";
            stmtTransRequest.StatementRequest.IncTransaction.EndDate = "20200611000000";
            stmtTransRequest.StatementRequest.IncTransaction.Include = true;

            document.Save(outputDir + @"newOfxRequestBankStatement.xml", OfxVersionEnum.V2x);
            document.Save(outputDir + @"newOfxRequestBankStatement.sgml", OfxVersionEnum.V1x);
            // ExEnd:1

            Console.WriteLine("CreateOfxBankTransactionRequestFile executed successfully.");
        }
    }
}
