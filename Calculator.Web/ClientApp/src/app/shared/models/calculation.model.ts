//Calculation shared model for the Calculator API consumption

import { CalculatorLog } from "./calculatorLog.model";

export class Calculation {
    public calculatorLog: CalculatorLog[];
    public response: number;
    public message: string;
}
