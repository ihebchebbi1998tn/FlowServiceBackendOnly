declare class HelperStore {
    static getStoreNameAbbrevation(StoreName: any): any;
    static mapForSelect2Display(store: any): {
        id: any;
        item: any;
        text: string;
    };
    static getAutocompleteFilter(query: any, term: any): any;
}
