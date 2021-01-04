using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.Data;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;
using ewApps.Core.CommonService;

namespace ewApps.AppPortal.DS {
    public class CustomerAccountDetailDS:BaseDS<CustomerAccountDetail>, ICustomerAccountDetailDS {

        #region Local Member

        ICustomerAccountDetailRepository _customeAccountDetailRepo;
        IUnitOfWork _unitOfWork;

        #endregion Local Member

        #region Constructor

        /// <summary>
        /// Initialinzing local variables .
        /// </summary>
        /// <param name="customeAccountDetailRepo"></param>
        /// <param name="unitOfWork"></param>
        public CustomerAccountDetailDS(ICustomerAccountDetailRepository customeAccountDetailRepo, IUnitOfWork unitOfWork) : base(customeAccountDetailRepo) {
            _customeAccountDetailRepo = customeAccountDetailRepo;
            _unitOfWork = unitOfWork;
        }

        #endregion Constructor

        public async Task<CustomeAccDetailDTO> GetCustomerAccDetailCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {

            CustomeAccDetailDTO customeAccDetailDTO = new CustomeAccDetailDTO();
            List<CustomerAccountDTO> customerAccountList = await _customeAccountDetailRepo.GetCustomerAccListByCustomerIdAsync(customerId, token);
            List<BankAcctDetailDTO> bankAcctDetailList = new List<BankAcctDetailDTO>();
            List<CreditCardDetailDTO> creditCardDetailList = new List<CreditCardDetailDTO>();
            CryptoHelper cryptoHelper = new CryptoHelper();
            if(customerAccountList != null) {
                foreach(CustomerAccountDTO accDetail in customerAccountList) {
                    if(accDetail.AccountType == (int)AccountTypeEnum.BankAccount) {
                        accDetail.AccountJson = cryptoHelper.Decrypt(accDetail.AccountJson, CryptoHelper.EncryptionAlgorithm.AES); // Decrypted account details
                        BankAcctDetailDTO bankAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<BankAcctDetailDTO>(accDetail.AccountJson);
                        bankAcctDetail.ID = accDetail.ID;
                        bankAcctDetailList.Add(bankAcctDetail);
                    }
                    else if(accDetail.AccountType == (int)AccountTypeEnum.CreditCard) {
                        accDetail.AccountJson = cryptoHelper.Decrypt(accDetail.AccountJson, CryptoHelper.EncryptionAlgorithm.AES); // Decrypted account details
                        CreditCardDetailDTO creditCardDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<CreditCardDetailDTO>(accDetail.AccountJson);
                        creditCardDetail.ID = accDetail.ID;
                        creditCardDetailList.Add(creditCardDetail);
                    }

                }
            }
            customeAccDetailDTO.BankAcctDetailList = bankAcctDetailList;
            customeAccDetailDTO.CreditCardDetailList = creditCardDetailList;
            return customeAccDetailDTO;
        }


        public async Task AddUpdateCustomerAcctDetail(Guid customerId, CustomeAccDetailDTO customeAccDetail, CancellationToken token = default(CancellationToken)) {
            CustomerAccountDetail customerAccountDetail = null;
            CryptoHelper cryptoHelper  =  new CryptoHelper();
            foreach(BankAcctDetailDTO bankaccDetail in customeAccDetail.BankAcctDetailList) {
                
                if(bankaccDetail.OperationType == (int)OperationType.Add) {
                    Guid bankAccountId = Guid.NewGuid();
                    bankaccDetail.ID = bankAccountId;
                    bankaccDetail.OperationType = (int)OperationType.Update;
                    string bankAcctjson = Newtonsoft.Json.JsonConvert.SerializeObject(bankaccDetail);
                    bankAcctjson = cryptoHelper.Encrypt(bankAcctjson, CryptoHelper.EncryptionAlgorithm.AES); // Encrypted bank account detail
                    customerAccountDetail = new CustomerAccountDetail();
                    UpdateSystemFieldsByOpType(customerAccountDetail, OperationType.Add);
                    customerAccountDetail.ID = bankAccountId;
                    customerAccountDetail.AccountJson = bankAcctjson;
                    customerAccountDetail.CustomerId = customerId;
                    customerAccountDetail.AccountType = (int)AccountTypeEnum.BankAccount;
                    await AddAsync(customerAccountDetail, token);
                }

                else if(bankaccDetail.OperationType == (int)OperationType.Update) {
                    customerAccountDetail = await FindAsync(custacc => custacc.ID == bankaccDetail.ID);
                    UpdateSystemFieldsByOpType(customerAccountDetail, OperationType.Update);
                    string bankAcctjson = Newtonsoft.Json.JsonConvert.SerializeObject(bankaccDetail);
                    bankAcctjson = cryptoHelper.Encrypt(bankAcctjson, CryptoHelper.EncryptionAlgorithm.AES); // Encrypted bank account detail
                    customerAccountDetail.AccountJson = bankAcctjson;
                    await UpdateAsync(customerAccountDetail, customerAccountDetail.ID);
                }
                else if(bankaccDetail.OperationType == (int)OperationType.Delete) {
                    customerAccountDetail = await FindAsync(custacc => custacc.ID == bankaccDetail.ID);
                    UpdateSystemFieldsByOpType(customerAccountDetail, OperationType.Update);
                    customerAccountDetail.Deleted = true;
                    await UpdateAsync(customerAccountDetail, customerAccountDetail.ID);
                }

            }
            foreach(CreditCardDetailDTO creditCardDetail in customeAccDetail.CreditCardDetailList) {

                // Masking Creditcard number
                creditCardDetail.CardNumber = MaskNumber(creditCardDetail.CardNumber);

                if(creditCardDetail.OperationType == (int)OperationType.Add) {
                    Guid creditCardId = Guid.NewGuid();
                    creditCardDetail.ID = creditCardId;
                    creditCardDetail.OperationType = (int)OperationType.Update;
                    string creditCardjson = Newtonsoft.Json.JsonConvert.SerializeObject(creditCardDetail);
                    creditCardjson = cryptoHelper.Encrypt(creditCardjson, CryptoHelper.EncryptionAlgorithm.AES); // Encrypted credit card detail
                    customerAccountDetail = new CustomerAccountDetail();
                    UpdateSystemFieldsByOpType(customerAccountDetail, OperationType.Add);
                    customerAccountDetail.ID = creditCardId;
                    customerAccountDetail.CustomerId = customerId;
                    customerAccountDetail.AccountType = (int)AccountTypeEnum.CreditCard;
                    customerAccountDetail.AccountJson = creditCardjson;
                    await AddAsync(customerAccountDetail, token);
                }
                else if(creditCardDetail.OperationType == (int)OperationType.Update) {
                    customerAccountDetail = await FindAsync(custacc => custacc.ID == creditCardDetail.ID);
                    UpdateSystemFieldsByOpType(customerAccountDetail, OperationType.Update);
                    string creditCardjson = Newtonsoft.Json.JsonConvert.SerializeObject(creditCardDetail);
                    creditCardjson = cryptoHelper.Encrypt(creditCardjson, CryptoHelper.EncryptionAlgorithm.AES); // Encrypted credit card detail
                    customerAccountDetail.AccountJson = creditCardjson;
                    await UpdateAsync(customerAccountDetail, customerAccountDetail.ID);
                }
                else if(creditCardDetail.OperationType == (int)OperationType.Delete) {
                    customerAccountDetail = await FindAsync(custacc => custacc.ID == creditCardDetail.ID);
                    UpdateSystemFieldsByOpType(customerAccountDetail, OperationType.Update);
                    customerAccountDetail.Deleted = true;
                    await UpdateAsync(customerAccountDetail, customerAccountDetail.ID);
                }
            }
            
            //save data
            _unitOfWork.SaveAll();
        }


        // <summary>
        /// Mask the number by X.
        /// </summary>
        /// <param name="cardNumber">card number</param>
        /// <returns></returns>
        public string MaskNumber(string cardNumber) {
            if(!string.IsNullOrEmpty(cardNumber) && cardNumber.Length > 4) {
                string value = "";
                for(int i = 0; i < cardNumber.Length - 4; i++) {
                    value += "x";
                }
                cardNumber = value + cardNumber.Substring(cardNumber.Length - 4);
            }

            return cardNumber;
        }

    }
}
