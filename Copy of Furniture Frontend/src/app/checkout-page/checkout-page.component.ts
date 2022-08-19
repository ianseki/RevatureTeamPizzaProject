
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { LoginPageComponent } from '../login-page/login-page.component';
import { DATA_Customer } from '../login-page/login-page.component';
import { APIService } from '../services/api.service';


@Component({
  selector: 'app-checkout-page',
  templateUrl: './checkout-page.component.html',
  styleUrls: ['./checkout-page.component.css']
})
export class CheckoutPageComponent implements OnInit {
  OrderHistoryData = new FormGroup({
    timeoforder: new FormControl(''),
    // status: new FormControl('')
  })

  OrderHistoryResponse: any;

  constructor(private router: Router, private API: APIService) { }

  ngOnInit(): void {
  }


  makePayment(){
    const Number_timeoforder = `${this.OrderHistoryData.value.timeoforder}`
    // const status: boolean = `${this.OrderHistoryData.value.status}`
    this.API.CUSTOMER_GetOrderHistory(2).subscribe((API_Response:any)=> {
      {
        alert("API_Response: " + API_Response)

        this.OrderHistoryResponse = API_Response;

        if(this.OrderHistoryResponse == true){
          alert("Thank you for your business");
          this.router.navigate(['home'])
        }
        else{
          const CustomerInfo: DMODEL_Order = this.OrderHistoryResponse;
          alert("");
          
        }

      }
    })

  }

  
  cancel(){
    this.router.navigate(["/home"]);

  }

  Homepage(){
    this.router.navigate(["/home"]);
  }


}

export interface DMODEL_Order{
  orderid: number
}
