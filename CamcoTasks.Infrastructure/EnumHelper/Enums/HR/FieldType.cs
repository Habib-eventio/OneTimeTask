namespace CamcoTasks.Infrastructure.EnumHelper.Enums.HR;


public enum FieldType
{
    [CustomDisplay("FirstName")] FirstName = 1,

    [CustomDisplay("LastName")] LastName = 2,

    [CustomDisplay("SocialSecurityNumber")]
    SocialSecurityNumber = 3,

    [CustomDisplay("Image")] Image = 4,

    [CustomDisplay("Gender")] Gender = 5,

    [CustomDisplay("DateOfBirth")] DateOfBirth = 6,

    [CustomDisplay("DateOfHire")] DateOfHire = 7,

    [CustomDisplay("JobType")] JobType = 8,

    [CustomDisplay("AssignShift")] AssignShift = 9,

    [CustomDisplay("Department")] Department = 10,

    [CustomDisplay("JobTitle")] JobTitle = 11,

    [CustomDisplay("EmployeeState")] EmployeeState = 12,

    [CustomDisplay("EmployeeType")] EmployeeType = 13,

    [CustomDisplay("TierLevel")] TierLevel = 14,

    [CustomDisplay("Manager")] Manager = 15,

    [CustomDisplay("IsSubcontractor")] IsSubcontractor = 16,

    [CustomDisplay("IsInternational")] IsInternational = 17,

    [CustomDisplay("State")] State = 18,

    [CustomDisplay("City")] City = 19,

    [CustomDisplay("ZipCode")] ZipCode = 20,

    [CustomDisplay("StreetAddress")] StreetAddress = 21,

    [CustomDisplay("Address")] Address = 22,

    [CustomDisplay("CellPhoneNumber")] CellPhoneNumber = 23,

    [CustomDisplay("HomePhoneNumber")] HomePhoneNumber = 24,

    [CustomDisplay("CamcoEmailAddress")] CamcoEmailAddress = 25,

    [CustomDisplay("PaymentType")] PaymentType = 26,

    [CustomDisplay("ShiftDifferential")] ShiftDifferential = 27,

    [CustomDisplay("InsuranceTypeId")] InsuranceTypeId = 28,

    [CustomDisplay("EmployeeShare")] EmployeeShare = 29,

    [CustomDisplay("CamcoShare")] CamcoShare = 30,

    [CustomDisplay("OtherHourlyBonus")] OtherHourlyBonus = 31,

    [CustomDisplay("OtherHourlyBonusDescription")]
    OtherHourlyBonusDescription = 32,

    [CustomDisplay("PercentOfDirectTime")] PercentOfDirectTime = 33,

    [CustomDisplay("AnnualLostTimeAllocationHours")]
    AnnualLostTimeAllocationHours = 34,

    [CustomDisplay("IsVacationDaysBase")] IsVacationDaysBase = 35,

    [CustomDisplay("AnnualVacationAllocation")]
    AnnualVacationAllocation = 36,

    [CustomDisplay("ReferredBy")] ReferredBy = 37,

    [CustomDisplay("DrugTestFrequency")] DrugTestFrequency = 38,

    [CustomDisplay("Notes")] Notes = 39,

    [CustomDisplay("BasePayRate")] BasePayRate = 40,

    [CustomDisplay("AnnualSalary")] AnnualSalary = 41,

    [CustomDisplay("SignedHandbookDocument")]
    SignedHandbookDocument = 42,

    [CustomDisplay("Leader")] Leader = 43,

    [CustomDisplay("ProbationEndDate")] ProbationEndDate = 44,

    [CustomDisplay("NewHirePaperworkPdf")] NewHirePaperworkPdf = 45,

    [CustomDisplay("NormalWorkHours")] NormalWorkHours = 46,

    [CustomDisplay("ShiftStartTime")] ShiftStartTime = 47,

    [CustomDisplay("EmployeeDeskStatus")] EmployeeDeskStatus = 48,

    [CustomDisplay("ShiftEndTime")] ShiftEndTime = 49,

    [CustomDisplay("SecondaryJobTitle")] SecondaryJobTitle = 50,

    [CustomDisplay("SecondaryLeader")] SecondaryLeader = 51,

    [CustomDisplay("SignedDocumentRequiredCheck")]
    SignedDocumentRequiredCheck = 52,

    [CustomDisplay("SubJobTitles")] SubJobTitles = 53,

    [CustomDisplay("DeskLocation")] DeskLocation = 54,

    [CustomDisplay("ShirtSize")] ShirtSize = 55,

    [CustomDisplay("CompanyNameId")] CompanyName = 56,

    [CustomDisplay("EmployeeQualityLevelStatus")]
    EmployeeQualityLevelStatus = 57,

    [CustomDisplay("EmployeeQualityLevel")]
    EmployeeQualityLevel = 58,

    [CustomDisplay("MailingAddressState")] MailingAddressState = 59,

    [CustomDisplay("MailingAddressCity")] MailingAddressCity = 60,

    [CustomDisplay("MailingAddressZipCode")] MailingAddressZipCode = 61,

    [CustomDisplay("MailingAddressStreetAddress")] MailingAddressStreetAddress = 62,

    [CustomDisplay("MailingAddress")] MailingAddress = 63,

    [CustomDisplay("HasPhone")] HasPhone = 64,

    [CustomDisplay("HasLaptop")] HasLaptop = 65,

    [CustomDisplay("HasVpnAccess")] HasVpnAccess = 66
}