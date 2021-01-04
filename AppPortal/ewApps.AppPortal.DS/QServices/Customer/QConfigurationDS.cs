using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.CommonService;

namespace ewApps.AppPortal.DS {
    public class QConfigurationDS : IQConfigurationDS {

        #region Local member

        IQConfigurationRepository _qConfigurationRepository;

        #endregion Local member

        #region cunstructor

        public QConfigurationDS(IQConfigurationRepository qConfigurationRepository) {
            _qConfigurationRepository = qConfigurationRepository;

        }

        #endregion cunstructor

        #region Get customer configuration

        /// <summary>
        /// Get configuration details of customer by customerid
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CustConfigurationViewDTO> GetConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get general details of customer
            CustConfigurationViewDTO custConfigurationViewDTO = await _qConfigurationRepository.GetConfigurationDetailAsync(buspartnertenantid, cancellationToken);

            // Get addresses details of customer
             List<CustCustomerAddressDTO> customerAddressDTOs = await _qConfigurationRepository.GetCustomerAddressListByIdAsync(custConfigurationViewDTO.CustomerID, cancellationToken);

            if(customerAddressDTOs != null) {
                foreach(CustCustomerAddressDTO custAddress in customerAddressDTOs) {
                    if(custAddress.ObjectTypeText.Equals("B")) {
                        custConfigurationViewDTO.CustomerBillAddressList.Add(custAddress);
                    }
                    else {
                        custConfigurationViewDTO.CustomerShipAddressList.Add(custAddress);
                    }

                }
            }

            // Get contact details of customer
            List<CustCustomerContactDTO> customerContactDTOs = await _qConfigurationRepository.GetCustomerContactListByIdAsync(custConfigurationViewDTO.CustomerID, cancellationToken);
            custConfigurationViewDTO.CustomerContactList = customerContactDTOs;

            

            // Get account details of customer
            CustomeAccDetailDTO customeAccDetailDTO = new CustomeAccDetailDTO();
            List<CustomerAccountDTO> customerAccountList = await _qConfigurationRepository.GetCustomerAccListByCustomerIdAsync(custConfigurationViewDTO.CustomerID, cancellationToken);
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
                        //custConfigurationViewDTO.BankAcctDetailList = bankAcctDetailList;
                    }
                    else if(accDetail.AccountType == (int)AccountTypeEnum.CreditCard) {
                        accDetail.AccountJson = cryptoHelper.Decrypt(accDetail.AccountJson, CryptoHelper.EncryptionAlgorithm.AES); // Decrypted account details
                        CreditCardDetailDTO creditCardDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<CreditCardDetailDTO>(accDetail.AccountJson);
                        creditCardDetail.ID = accDetail.ID;
                        creditCardDetailList.Add(creditCardDetail);
                        //custConfigurationViewDTO.CreditCardDetailList = creditCardDetailList;
                    }
                }
            }
            customeAccDetailDTO.BankAcctDetailList = bankAcctDetailList;
            customeAccDetailDTO.CreditCardDetailList = creditCardDetailList;

            custConfigurationViewDTO.CustomerBankAcctDetailList = customeAccDetailDTO.BankAcctDetailList;
            custConfigurationViewDTO.CustomerCreditCardDetailList = customeAccDetailDTO.CreditCardDetailList;
            //return customeAccDetailDTO;
            
            return custConfigurationViewDTO;

        }

        #endregion Get customer configuration

        #region Get Vendor configuration

        /// <summary>
        /// Get configuration details of customer by customerid
        /// </summary>
        /// <param name="buspartnertenantid"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<VendorConfigurationDTO> GetVendorConfigurationDetailAsync(Guid buspartnertenantid, CancellationToken cancellationToken = default(CancellationToken)) {

            // Get general details of customer
            VendorConfigurationDTO vendorConfigurationDTO = await _qConfigurationRepository.GetVendorConfigurationDetailAsync(buspartnertenantid, cancellationToken);

            // Get addresses details of customer
            List<VendorAddressDTO> vendorAddressDTOs = await _qConfigurationRepository.GetVendorAddressListByIdAsync(vendorConfigurationDTO.VendorID, cancellationToken);

            if(vendorAddressDTOs != null) {
                foreach(VendorAddressDTO custAddress in vendorAddressDTOs) {
                    if(custAddress.ObjectTypeText.Equals("B")) {
                        vendorConfigurationDTO.VendorBillAddressList.Add(custAddress);
                    }
                    else {
                        vendorConfigurationDTO.VendorShipAddressList.Add(custAddress);
                    }

                }
            }

            // Get contact details of customer
            List<VendorContactDTO> vendorContactDTOs = await _qConfigurationRepository.GetVendorContactListByIdAsync(vendorConfigurationDTO.VendorID, cancellationToken);
            vendorConfigurationDTO.VendorContactList = vendorContactDTOs;



            // Get account details of customer
            CustomeAccDetailDTO customeAccDetailDTO = new CustomeAccDetailDTO();
            List<CustomerAccountDTO> customerAccountList = await _qConfigurationRepository.GetCustomerAccListByCustomerIdAsync(vendorConfigurationDTO.VendorID, cancellationToken);
            List<BankAcctDetailDTO> bankAcctDetailList = new List<BankAcctDetailDTO>();
            List<CreditCardDetailDTO> creditCardDetailList = new List<CreditCardDetailDTO>();
            //CryptoHelper cryptoHelper = new CryptoHelper();
            //if(customerAccountList != null) {
            //    foreach(CustomerAccountDTO accDetail in customerAccountList) {
            //        if(accDetail.AccountType == (int)AccountTypeEnum.BankAccount) {
            //            accDetail.AccountJson = cryptoHelper.Decrypt(accDetail.AccountJson, CryptoHelper.EncryptionAlgorithm.AES); // Decrypted account details
            //            BankAcctDetailDTO bankAcctDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<BankAcctDetailDTO>(accDetail.AccountJson);
            //            bankAcctDetail.ID = accDetail.ID;
            //            bankAcctDetailList.Add(bankAcctDetail);
            //            //custConfigurationViewDTO.BankAcctDetailList = bankAcctDetailList;
            //        }
            //        else if(accDetail.AccountType == (int)AccountTypeEnum.CreditCard) {
            //            accDetail.AccountJson = cryptoHelper.Decrypt(accDetail.AccountJson, CryptoHelper.EncryptionAlgorithm.AES); // Decrypted account details
            //            CreditCardDetailDTO creditCardDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<CreditCardDetailDTO>(accDetail.AccountJson);
            //            creditCardDetail.ID = accDetail.ID;
            //            creditCardDetailList.Add(creditCardDetail);
            //            //custConfigurationViewDTO.CreditCardDetailList = creditCardDetailList;
            //        }
            //    }
            //}
            //customeAccDetailDTO.BankAcctDetailList = bankAcctDetailList;
            //customeAccDetailDTO.CreditCardDetailList = creditCardDetailList;

            //custConfigurationViewDTO.CustomerBankAcctDetailList = customeAccDetailDTO.BankAcctDetailList;
            //custConfigurationViewDTO.CustomerCreditCardDetailList = customeAccDetailDTO.CreditCardDetailList;
            ////return customeAccDetailDTO;

            return vendorConfigurationDTO;

        }

        #endregion Get Vendor configuration

    }
}
