import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Calculation } from "../shared/models/calculation.model";
import { CalculatorLog } from "../shared/models/calculatorLog.model";

@Component({
    selector: 'calculator',
    templateUrl: './calculator.component.html',
})
export class CalculatorComponent {

    public message: string;
    calculation : Calculation;
    calc: CalculatorLog;
    http: HttpClient;
    baseUrl: string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.calc = new CalculatorLog();
        this.http = http;
        this.baseUrl = baseUrl;
        this.calculation = new Calculation();
    }

    getEntries() {
      this.http.get<Calculation>(this.baseUrl + 'calculator').subscribe(result => {
          this.calculation = result;
          this.message = result.message;
      }, error => console.error(error));
    }

    calculate(first: number, second: number) {

      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })};

        this.calc.calculation = first + " + " + second;

      this.http.post<Calculation>(this.baseUrl + 'calculator', this.calc, httpOptions).subscribe(result => {
          this.calculation = result;
          this.message = result.message;
      }, error => console.error(error));
    }


}


