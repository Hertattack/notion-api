export default class RequestError extends Error {
    public data: any;

    constructor(message: string, data: any) {
        super(message);
        this.data = data;
    }
}