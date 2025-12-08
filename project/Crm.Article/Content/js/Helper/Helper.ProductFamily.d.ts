declare class HelperProductFamily {
    static filterParent(query: any, term: any, id: any): any;
    static filterChild(query: any, term: any, id: any): any;
    static getProductFamilyAutocompleteFilter(query: any, filter: any, language: any): any;
    static loadProductFamilyDescriptions(results: any, language: any): any;
    static getProductFamilyAutocompleteDisplay(productfamily: any): any;
    static getProductFamilyDescription(productfamily: any): any;
    static getProductFamilyStatusAbbrevation(productFamily: any): any;
    static getParent(productFamilyId: any, viewModel: any): any;
}
