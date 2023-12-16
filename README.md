![Nom de l'image](https://cdn.discordapp.com/attachments/517055230756782095/1184904638990921809/mymenu-ico.png?ex=658dab70&is=657b3670&hm=c4dd168c7ae7252d4da6d53868f151fee5b9a8da544ecd7f3df0360467d55ffe&)

# Novalife Plugin MyMenu

Ce plugin Novalife MyMenu fournit la base d'un menu sur lequel les diff�rents d�veloppeurs de plugins peuvent impl�menter leurs sections respectives.
L'objectif est d'utiliser le m�me menu afin d'�viter les conflits potentiels entre nos plugins.

## Table des Mati�res

- [Installation](#installation)
- [Utilisation](#utilisation)
- [Fonctionnalit�s](#fonctionnalit�s)
- [Droits de Propri�t� Intellectuelle](#droits-de-propri�t�-intellectuelle)

## Installation

1. T�l�chargez le fichier `MyMenu.dll` depuis la page des releases de ce d�p�t. 
2. Ajoutez le fichier `MyMenu.dll` dans le dossier des plugins de votre serveur Novalife.

## Utilisation

Le Plugin MyMenu instaure la base d'un menu commun.  
Pour l'utiliser, suivez ces �tapes :

1. Int�grez le Plugin MyMenu.dll en tant que r�f�rence dans votre projet Novalife.

## Fonctionnalit�s

Placez votre code � l'int�rieur de la fonction OnPluginInit.

```csharp
public override void OnPluginInit()
{
	// Commencer par cr�er une section en renseignant la version de votre plugin et votre pseudo
	Section section = new Section(Section.GetSourceName(), Section.GetSourceName(), "v0.0.0", "Author");

	// Cr�er une expression lambda prenant UIPanel en param�tre et renseigner votre fonction principale
	Action<UIPanel> action = ui => VotreFonction(section.GetPlayer(ui));

	//-------------------------------------------------------------------

	// (Facultatif) CONDITIONS PAR D�FAUT:
	// Quels sont les identifiants des soci�t�s ayant acc�s � votre section ? (Ne rien indiquer n'applique pas de condition)
	// exemple:
	section.SetBizIdAllowed(1, 4);

	// Quels sont les types de soci�t�s ayant acc�s � votre section ? (Ne rien indiquer n'applique pas de condition)
	// exemple:
	section.SetBizTypeAllowed(Activity.Type.Chef, Activity.Type.Electrician);

	// Est-ce que votre section est accessible uniquement par des administrateurs ? (false par d�faut)
	// exemple:
	section.OnlyAdmin = true;

	// Quel rang minimum requis d'un administrateur pour acc�der � votre section ? (0 par d�faut)
	// exemple:
	section.MinAdminLevel = 5;

	//-------------------------------------------------------------------

	// Cr�ation de votre TabLine et insertion dans MyMenu (Renseigner true en param�tre de la fonction Insert SI votre plugin est destin� aux administrateurs)
	section.Line = new UITabLine(section.Title, action);
	section.Insert(false);
}
```

## Droits de Propri�t� Intellectuelle

Je vous demande simplement de respecter le temps que j'ai mis dans ce plugin.  
Merci de ne pas vous approprier le plugin, de ne pas le copier b�tement, et de ne pas faire des trucs �tranges avec.

Pour discuter, contactez-moi sur discord: Aarnow  
Serveur discord: https://discord.gg/8j2suEE9Mf