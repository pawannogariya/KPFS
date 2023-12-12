import { IDto } from './base.dto';

export interface IResponse<TData> extends IDto {
  data: TData;
  dataTotalCount: number;
  isSuccess: boolean;
  message: string;
  systemMessage: string;
  errors: any;
}

export class DataDtoRange {
  public total: number;
  public offset: number = 0;
  public limit: number = 20;

  private defaultOffset: number = 0;
  private defaultLimit: number = 20;

  public constructor(offset: number = 0, limit: number = 20) {
    this.defaultOffset = offset;
    this.defaultLimit = limit;
    this.reset();
  }

  public reset() {
    this.offset = this.defaultOffset;
    this.limit = this.defaultLimit;
  }
}

export class DataDtoFilter {
  public search: string;
  public sortBy: string;
  public sortDirection: string;

  public multiSort: { [key: string]: ISortItem } = {};

  public constructor() {
  }

  public getEncodedSearch(): string {
    return this.search ? encodeURIComponent(this.search) : null;
  }

  public getMultiSortArray(): string[] {
    let multiSort: string[] = [];
    for (const key in this.multiSort) {
      const sortItem = this.multiSort[key] as ISortItem;
      if (sortItem.sortDirection != '') {
        multiSort.push(`${sortItem.sortBy}:${sortItem.sortDirection}`);
      }
    }
    return multiSort;
  }
}

export interface ISortItem {
  sortBy: string;
  sortDirection: string;
}

