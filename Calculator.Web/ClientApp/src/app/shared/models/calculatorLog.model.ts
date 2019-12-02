//CalculatorLog shared model for the Calculator API response

export class CalculatorLog {
    public id: number;
    public ipAddress: string;
    public timestamp: Date;
    public calculation: string;
    public result: string;
}
