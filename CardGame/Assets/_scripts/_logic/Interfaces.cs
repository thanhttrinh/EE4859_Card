using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter: IIdentifiable {
	//int health {get; set;}

	//void Die();
}

public interface IIdentifiable{
	int ID{ get;}
}
