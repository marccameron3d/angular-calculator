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
    equation : string;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
        this.calc = new CalculatorLog();
        this.http = http;
        this.baseUrl = baseUrl;
        this.calculation = new Calculation();
        this.equation = "";
    }

    add(character : string) {
      this.equation += character;
    }

    clear() {
      this.equation = "";
    }

    getEntries() {
      this.http.get<Calculation>(this.baseUrl + 'calculator').subscribe(result => {
          this.calculation = result;
          this.message = result.message;
      }, error => console.error(error));
    }

    calculate(expression : string) {

      const httpOptions = {
        headers: new HttpHeaders({
          'Content-Type': 'application/json'
        })};

      this.calc.calculation = expression;

      this.http.post<Calculation>(this.baseUrl + 'calculator', this.calc, httpOptions).subscribe(result => {
          this.calculation = result;
          this.message = result.message;
          this.equation = result.response[0].result;
      }, error => console.error(error));
    }


}


