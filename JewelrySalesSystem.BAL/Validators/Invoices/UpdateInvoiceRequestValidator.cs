﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using JewelrySalesSystem.BAL.Interfaces;
using JewelrySalesSystem.BAL.Models.Invoices;

namespace JewelrySalesSystem.BAL.Validators.Invoices
{
    public class UpdateInvoiceRequestValidator : AbstractValidator<UpdateInvoiceRequest>
    {
        //Initialize for using to method for checking exist
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;
        private readonly IWarrantyService _warrantyService;
        private readonly IProductService _productService;
        private readonly IInvoiceService _invoiceService;
        public UpdateInvoiceRequestValidator(ICustomerService customerService, IUserService userService, IWarrantyService warrantyService, IProductService productService, IInvoiceService invoiceService)
        {
            _customerService = customerService;
            _userService = userService;
            _productService = productService;
            _warrantyService = warrantyService;
            _invoiceService = invoiceService;

            //Validate InvoiceId in InvoiceDetails: Not Empty, is integer, >0, is existed
            RuleFor(i => i.InvoiceId).NotEmpty().WithMessage("Invoice Id is required")
                .Must(MustBeAnId).WithMessage("Invoice Id must be an integer and greater than 0")
                .MustAsync(async (invoiceId, cancelationToken) => await beValidInvoice(invoiceId, cancelationToken)).WithMessage("Invoice Id is not existed");

            //Validate CustomerName: Not Empty, is existed
            RuleFor(i => i.CustomerName).NotEmpty().WithMessage("CustomerName is required")
                .MustAsync(async (customerName, cancelationToken) => await beValidCustomer(customerName, cancelationToken)).WithMessage("Customer is not existed");

            //Validate UserId: Not Empty, is integer, >0, is existed
            RuleFor(i => i.UserId).NotEmpty().WithMessage("User Id is required")
                .GreaterThan(0).WithMessage("User Id must greater than 0")
                .MustAsync(async (userId, cancellationToken) => await beValidUser(userId, cancellationToken)).WithMessage("User Id is not existed");

            //Validate WarrantyId: Not Empty, is integer, >0, is existed
            RuleFor(i => i.WarrantyId).NotEmpty().WithMessage("Warranty Id is required")
                .GreaterThan(0).WithMessage("Warranty Id must greater than 0")
                .MustAsync(async (warrantyId, cancellationToken) => await beValidWarranty(warrantyId, cancellationToken)).WithMessage("Warranty Id is not existed");

            //Validate ProductId in InvoiceDetails: Not Empty, is integer, >0, is existed
            RuleForEach(i => i.InvoiceDetails).NotEmpty().WithMessage("Values in InvoiceDetails is not null")
                .GreaterThan(0).WithMessage("Values in InvoiceDetails must greater than 0")
                .MustAsync(async (productId, cancellationToken) => await beValidProduct(productId, cancellationToken)).WithMessage($"Product Id is not existed");

        }


        //Check Invoice is existed by id
        private async Task<bool> beValidInvoice(int id, CancellationToken cancellationToken)
        {
            var checkExist = await _invoiceService.GetByIdAsync(id);
            if (checkExist != null)
            {
                return true;
            }
            return false;
        }

        //Check User is existed by id
        private async Task<bool> beValidUser(int id, CancellationToken cancellationToken)
        {
            var checkExist = await _userService.GetByIdAsync(id);
            if (checkExist != null)
            {
                return true;
            }
            return false;
        }

        //Check Warranty is existed by id
        private async Task<bool> beValidWarranty(int id, CancellationToken cancellationToken)
        {
            var checkExist = await _warrantyService.GetWarrantyById(id);
            if (checkExist != null)
            {
                return true;
            }
            return false;
        }

        //Check Customer is existed by name
        private async Task<bool> beValidCustomer(string customerName, CancellationToken cancellationToken)
        {
            var checkExist = await _customerService.GetCustomerByNameAsync(customerName);
            if (checkExist != 0)
            {
                return true;
            }
            return false;
        }

        //Check Product is existed by id
        private async Task<bool> beValidProduct(int id, CancellationToken cancellationToken)
        {
            var checkExist = await _productService.GetProductByIdAsync(id);
            if (checkExist != null)
            {
                return true;
            }
            return false;
        }

        //Condition for input id
        private static bool MustBeAnId<T>(T value)
        {
            if (value == null)
                return true;

            if (value is int intValue && intValue > 0)
                return true;

            //if (value is string stringValue)
            //    return int.TryParse(stringValue, out _);

            return false;
        }
    }
}