namespace FULFILLMENT.H4U.API.Model.Constants
{
    public class Constants
    {
        public const string ROLE_FOR_COMPANY_ONLY = "VENDOR | SALE";

        public const string RESPONSE_UNIT = "Seconds.";

        public const string DATA_NOT_FOUND = "DATA NOT FOUND.";


        public const string GET_DATA_SUCCESS = "GET DATA SUCCESS";
        public const string UPDATE_SUCCESS = "UPDATED SUCCESS";
        public const string UPDATE_DATA_INVALID = "DATA UPDATE IS INVALID PLEASE CHECK AND RE-TRY AGAIN.";
        public const string INSERT_SUCCESS = "INSERT SUCCESS";
        public const string DELETE_SUCCESS = "DELETE SUCCESS";
        public const string INSERT_DUPLICATE = "DATA INSERT IS ALREADY PLEASE CHECK AND RE-TRY AGAIN.";
        public const string UPDATE_DUPLICATE = "DATA UPDATE IS ALREADY PLEASE CHECK AND RE-TRY AGAIN.";
        public const string INSERT_DATA_INVALID = "DATA INSERT IS INVALID PLEASE CHECK AND RE-TRY AGAIN.";

        public const string GET_DATA_ERRO = "GET DATA FAILD PLEASE CONTACT ADMINISTATOR SYSTEM.";
        public const string UPDATE_ERROR = "UPDATE DATA FAILD PLEASE CONTACT ADMINISTATOR SYSTEM.";
        public const string INSERT_ERROR = "INSERT DATA FAILD PLEASE CONTACT ADMINISTATOR SYSTEM.";
        public const string DELETE_ERROR = "DELETE DATA FAILD PLEASE CONTACT ADMINISTATOR SYSTEM.";


        public const string SIGN_IN_SUCCESS = "SIGN IN SUCCESS";
        public const string SIGN_IN_ERROR = "USERNAME OR PASSWORD INCORRECT. PLEASE CHECK DATA AND RE-TRY AGAIN.";
        public const string ACCOUNT_BANNED = "YOUR ACCOUNT BANNED OR SUSPENDED. PLEASE CONTACT ADMINISTATOR SYSTEM.";

        public const string SIGN_UP_SUCCESS = "SIGN UP SUCCESS";
        public const string SIGN_UP_ERROR = "SIGN UP FAILD. PLEASE CONTACT ADMINISTATOR SYSTEM.";
        public const string SIGN_UP_USER_VENDOR_FAILD = "PLEASE SELECT YOUR COMPANY OR CONTACT ADMINISTATOR SYSTEM.";
        public const string SIGN_UP_USER_SECRET_FAILD = "PLEASE CHECK YOUR SECRET KEY.";
        public const string SIGN_UP_EMAIL_ALREADY = "EMAIL IS ALREADY.";
        public const string SIGN_UP_USERNAME_ALREADY = "USERNAME IS ALREADY.";

        public const string DATA_TYPE_ERROR = "INVALID DATA TYPE";
        public const string DATA_FORM_ERROR = "INVALID DATA FORM";

        public const string DOCUMENT_TYPE_INBOUND = "IN";
        public const string DOCUMENT_TYPE_PURCHASE_ORDER = "PO";
        public const string DOCUMENT_TYPE_GOOD_RECEIVE = "GR";
        public const string DOCUMENT_TYPE_LOT = "LT";
        public const string DOCUMENT_TYPE_PICK_LIST = "PL";
        public const string DOCUMENT_TYPE_PACKING = "PCK";


        public const string NOT_FOUND_PRODUCT_WITH_IN_YOUR_MASTER_PRODUCT = "NOT FOUND PRODUCT WITH IN YOUT MASTER PRODUCT";
        public const string NOT_FOUND_PRODUCT_WITH_IN_STOCK = "NOT FOUND PRODUCT WITH IN STOCK";
        public const string NOT_FOUND_LOCATION_WITH_IN_MASTER = "NOT FOUND LOCATION WITH IN LOCATION PRODUCT";
        public const string NOT_FOUND_SHEFT_WITH_IN_MASTER = "NOT FOUND SHEFT WITH IN LOCATION PRODUCT";
        public const string NOT_FOUND_VENDOR_WITH_IN_MASTER = "NOT FOUND VENDOR DATA WITH IN MASTER SYSTEM.";
        public const string ITEM_STOCK_NOT_ENOUGH = "a vendor's inventory is below zero.";



        #region Functio Control

        public const string FUNCTION_SIGN_IN = "SIGN IN";

        #endregion

        #region Transaction Inbound

        public const string INBOUND_STATUS_SHIPPING = "SHIPPING";
        public const string INBOUND_STATUS_ARRIVED = "ARRIVED";
        public const string INBOUND_STATUS_RECEIVED = "RECEIVED";
        public const string INBOUND_STATUS_CANCEL = "CANCEL";

        public const string INBOUND_UPDATE_DOCUMENT_FAILD = "CAN YOU UPDATE DOCUMENT STATUS IS SHIPPING ONLY.";
        public const string AMOUNT_FOR_GR_MORE_THAN_AMOUNT_OF_ITEM_INBOUND = "AMOUNT FOR GR MORE THAN AMOUNT OF ITEM INBOUND";
        public const string ITEM_GR_IS_ALREADY = "ITEM GR IS ALREADY";
        public const string AMOUNT_FOR_GR_IS_REQUIRE = "AMOUNT FOR GR IS REQUIRE";
        public const string AMOUNT_IS_REQUIRE = "AMOUNT IS REQUIRE";

        #endregion



        #region Transaction Goods Receive

        public const string GOOD_RECEIVE_PRODUCT_WITH_INVALID = "CAN'T INSERT DATA BECAUSE BIN IS REQUIRE FOR WIDTH OF PRODUCT LESS THEN SPECIFICATION";
        public const string GOOD_RECEIVE_CAN_NOT_UPDATE_STOCK = "CAN'T STOCK DATA BECAUSE NOT FOUND STOCK RECORD IN SYSTEM. PLEASE CONTACT ADMINISTTOR SYSYEM";
        public const string GOOD_RECEIVE_CAN_NOT_UPDATE_ITEM_STATUS_INVALID = "CAN'T UPDATE DATA BECAUSE STATUS IS INCORRECT.";

        public const string GOOD_RECEIVE_STATUS_RECEIVED = "RECEIVED";
        public const string GOOD_RECEIVE_STATUS_REJECT = "REJECT";
        public const string GOOD_RECEIVE_STATUS_CANCEL = "CANCEL";
        public const string GOOD_RECEIVE_STATUS_QC = "QC";
        public const string GOOD_RECEIVE_STATUS_HOLD = "HOLD";
        public const string GOOD_RECEIVE_STATUS_PENDING = "PENDING";
        public const string GOOD_RECEIVE_STATUS_SUCCESS = "SUCCESS";
        public const string NULL = "N/A";


        #endregion


        #region Transaction Purchase Order

        public const string PURCHASE_ORDER_STATUS_PENDING = "PENDING";
        public const string PURCHASE_ORDER_STATUS_PICKLIST = "PICKLIST";
        public const string PURCHASE_ORDER_STATUS_PACKING = "PACKING";
        public const string PURCHASE_ORDER_STATUS_SHIPPED = "SHIPPED";
        public const string PURCHASE_ORDER_STATUS_CANCEL = "CANCEL";

        public const string PURCHASE_ORDER_INFORMATION_CUSTOMER_IS_REQUIRE = "INFORMATIONCUSTOMER_IS_REQUIRE";
        public const string PURCHASE_ORDER_INFORMATION_SHIPPNNG_IS_REQUIRE = "INFORMATION_SHIPPNNG_IS_REQUIRE";
        public const string PURCHASE_ORDER_INFORMATION_IS_SHIPPED = "THIS PURCHASE ORDER IS SHIPPED";


        #endregion


        #region Transaction Pick List

        public const string PICK_LIST_PO_NOT_FOUND = "NOT FOUND ORDER ID";

        #endregion


        #region Transaction Packing

        public const string NOT_FOUND_BIN_IN_PICKLIST_PROCESS = "NOT FOUND BIN IN PICKLIST PROCESS";
        public const string NOT_FOUND_PURCHASE_ORDER = "NOT FOUND PURCHASE ORDER";



        #endregion



        public const string FILL_OUT_THE_REQUIRE = "Please fill out the require.";

        public const string SIGN_UP_INITIAL_ROLE = "PENDING";

        public const int CONDITION_WITH_PRODUCT = 25;

        public const string STOCK_MANUAL_TYPE_ADD = "ADD";
        public const string STOCK_MANUAL_TYPE_DELETE = "DELETE";
        public const string STOCK_MANUAL_TYPE_INVALID = "STOCK MANUAL TYPE INVALID";
        public const string NOT_FOUND_FILE_UPLOAD = "Not found file upload.";
    }
}
