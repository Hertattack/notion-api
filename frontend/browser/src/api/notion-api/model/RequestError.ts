export default class RequestError extends Error {

    // eslint-disable-next-line @typescript-eslint/no-unused-vars
    constructor(message: string, _data: unknown | undefined) {
        super(message);
    }
}