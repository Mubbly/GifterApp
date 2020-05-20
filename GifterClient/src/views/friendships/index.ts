import { autoinject } from "aurelia-framework";
import { IFriendship } from "domain/IFriendship";
import { FriendshipService } from "service/friendshipService";
import * as UtilFunctions from "utils/utilFunctions";
import { Optional } from "types/generalTypes";

@autoinject
export class FriendshipsIndex {
    private _reservedFriendships: IFriendship[] = [];
    private _errorMessage: Optional<string> = null;

    constructor(private friendshipService: FriendshipService) {}

    attached() {
        this.getFriendships();
    }

    private getFriendships(): void {
        this.friendshipService.getAll().then((response) => {
            if (UtilFunctions.isSuccessful(response)) {
                this._reservedFriendships = response.data!;
            } else {
                this._errorMessage = UtilFunctions.getErrorMessage(response);

            }
        });
    }
}
