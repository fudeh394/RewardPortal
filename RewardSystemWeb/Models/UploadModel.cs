using RewardSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RewardSystemWeb.Models
{
   
    public class UploadModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public Guid BatchNumber { get; set; }
        public bool  IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int FlowStatus { get; set; }
        public bool IsBatchApproved { get; set; }
        public bool IsBatchValidated { get; set; }
        public string RMStaffID { get; set; }
        public string DeskCode { get; set; }
        public string TeamCode { get; set; }
        public string BUCode { get; set; }
        public string PayerName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string PayerAccountNumber { get; set; }
        public DateTime PayementDate { get; set; }
        public string PayerType { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string InitiatingBranchName { get; set; }
        public string InitiatingBranchSolID { get; set; }
        public string ProductCategory { get; set; }
        public string Month { get; set; }
        public string SolId { get; internal set; }
        public string BeneficiaryName { get; set; }
        public string CollectionPlatform { get; internal set; }
    }

    public class ReportModel
    {
        [Column("RM Code")]
        public string RMCode { get; set; }
        [Column("Desk Code")]
        public string DeskCode { get; set; }
        [Column("Team Code")]
        public string TeamCode { get; set; }
        [Column("BU Code")]
        public string BUCode { get; set; }
        [Column("Payer Name")]
        public string PayerName { get; set; }

        [Column("Payer Account Number")]
        public string PayerAccountNumber { get; set; }

        [Column("Beneficiary Name")]
        public string BeneficiaryName { get; set; }

        [Column("Beneficiary Account Number")]
        public string BeneficiaryAccountNumber { get; set; }

        [Column("Payment Date")]
        public string PaymentDate { get; set; }

        [Column("Payment Type")]
        public string PaymentType { get; set; }

        [Column("Payment ReferenceNo")]
        public string PaymentReferenceNo { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("Initiating Branch Name")]
        public string InitiatingBranchName { get; set; }
        [Column("Initiating Branch SOL ID")]
        public string InitiatingBranchSolID { get; set; }
        [Column("Product Category")]
        public string ProductCategory { get; set; }
        [Column("Collection Platform")]
        public string CollectionPlatform { get; set; }
    }





    public class BillingResult
    {
        [Column("S/N")]
        public string SN { get; set; }

        [Column("AGT MGR INST NAME")]
        public string AgrntMgrInstName { get; set; }
        [Column("AGT MGR INST CODE")]
        public string AgrntMgrInstCode { get; set; }

        [Column("AMOUNT")]
        public decimal Amount { get; set; }
        [Column("ACCOUNT NUMBER")]
        public string AccountNumber { get; set; }

        [Column("ACCOUNT NAME")]
        public string AccountName { get; set; }

        [Column("BANK CODE")]
        public string BankCode { get; set; }

        [Column("NARRATION")]
        public string Narration { get; set; }


    }

    public class BillingReport
    {
        [Column("AGT MGR INST NAME")]
        public string AgtMgrInstName { get; set; }

        [Column("SN")]
        public string SN { get; set; }

        [Column("AGT MGR INST CODE")]
        public string AgtMgrInstCode { get; set; }

        [Column("AMOUNT")]
        public decimal Amount { get; set; }
        [Column("ACCOUNT NUMBER")]
        public string AccountNumber { get; set; }

        [Column("ACCOUNT NAME")]
        public string AccountName { get; set; }

        [Column("BANK CODE")]
        public string BankCode { get; set; }

        [Column("NARRATION")]
        public string Narration { get; set; }

        [Column("COUNT")]
        public int Count { get; set; }


    }




    public class ReportRequestModel
    {
        public string AccountNumber { get; set; }
        public string ChequeNumber { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string SFromDate
        {
            get
            {
                if (this.FromDate != null)
                    return FromDate.ToString("yyyy-MM-dd");
                return DateTime.Now.ToString("yyyy-MM-dd");
            }


        }

        public string SToDate
        {
            get
            {
                if (this.ToDate != null)
                    return ToDate.ToString("yyyy-MM-dd");
                return DateTime.Now.ToString("yyyy-MM-dd");
            }

        }

        public bool Preview { get; set; }
        public bool Download { get; set; }
        public string SolId { get; set; }
        public string UserId { get; set; }
        public string DeskCode { get; set; }
        public string WindowName { get; set; }
    }

    public class Request
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid BatchNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RequestStatus { get; set; }
        public string FilePath { get; set; }
        public string SolId { get; internal set; }
        public string CreatedBy { get; set; }
        public string OriginalFileName { get; internal set; }
    }

    public class HoRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public Guid BatchNumber { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RequestStatus { get; set; }
        public string FilePath { get; set; }
        public string SolId { get; internal set; }
        public string CreatedBy { get; set; }
        public string OriginalFileName { get; internal set; }
    }

    class UserExcelFormat
    {
        [Column("UserId")]
        public string UserId { get; set; }
       

    }
    class CustomExcelFormat
    {
        [Column("RM Code")]
        public string RMCode { get; set; }    
        [Column("Desk Code")]
        public string DeskCode { get; set; }
        [Column("Team Code")]
        public string TeamCode { get; set; }
        [Column("BU Code")]
        public string BUCode { get; set; }
        [Column("Payer Name")]
        public string PayerName { get; set; }

        [Column("Payer Account Number")]
        public string PayerAccountNumber { get; set; }

        [Column("Beneficiary Name")]
        public string BeneficiaryName { get; set; }

        [Column("Beneficiary Account Number")]
        public string BeneficiaryAccountNumber { get; set; }
        
        [Column("Payment Date")]
        public string   PaymentDate { get; set; }

        [Column("Payment Type")]
        public string PaymentType { get; set; }

        [Column("Payment ReferenceNo")]
        public string PaymentReferenceNo { get; set; }

        [Column("Amount")]
        public decimal  Amount { get; set; }

        [Column("Initiating Branch Name")]
        public string InitiatingBranchName { get; set; }
        [Column("Initiating Branch SOL ID")]
        public string InitiatingBranchSolID { get; set; }
        [Column("Product Category")]
        public string ProductCategory { get; set; }     
        [Column("Collection Platform")]
        public string  CollectionPlatform { get; set; }

        [Column("Branch Request ID")]
        public long BranchRequestID { get; set; }
        
    }


    public class CreatePost
    {
        public long Id { get; set; }
        public Guid BatchNumber { get; set; }
        public bool IsDeleted { get; set; }
        public string  CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int RequestStatus { get; set; }
        public HttpPostedFileBase FilePath { get; set; }
        public string   Path { get; set; }
        public string SolId { get; internal set; }
        public string OriginalFileName { get; internal set; }
        public List<BillingResult> Items { get; set; }
        
    }

    public class VerifiedRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ID { get; set; }
        public long BranchRequestID { get; set; }
        public Guid BatchNumber { get; set; }      
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public int FlowStatus { get; set; }
        public bool IsBatchApproved { get; set; }
        public bool IsBatchValidated { get; set; }
        public string RMStaffID { get; set; }
        public string DeskCode { get; set; }
        public string Month { get; set; }
        public string TeamCode { get; set; }
        public string BUCode { get; set; }
        public string PayerName { get; set; }
        public string BeneficiaryAccountNumber { get; set; }
        public string PayerAccountNumber { get; set; }
        public DateTime PayementDate { get; set; }
        public string PayerType { get; set; }
        public string PaymentReferenceNumber { get; set; }
        public decimal Amount { get; set; }
        public string InitiatingBranchName { get; set; }
        public string InitiatingBranchSolID { get; set; }
        public string ProductCategory { get; set; }
        public string BeneficiaryName { get; set; }
        public string CollectionPlatform { get; internal set; }
    }
}