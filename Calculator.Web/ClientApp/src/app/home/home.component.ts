import { Component, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Calculation } from "../shared/models/calculation.model";

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
})
export class HomeComponent {

    public getCalculation: Calculation;
    public postCalculation: Calculation;

    constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {

        http.get<Calculation>(baseUrl + 'calculator').subscribe(result => {
            this.getCalculation = result;
        }, error => console.error(error));

        const httpOptions = {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };

        http.post<Calculation>(baseUrl + 'calculator', null, httpOptions).subscribe(result => {
            this.postCalculation = result;
        }, error => console.error(error));

    }
}


