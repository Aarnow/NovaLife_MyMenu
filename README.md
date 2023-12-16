![Nom de l'image](https://cdn.discordapp.com/attachments/517055230756782095/1184904638990921809/mymenu-ico.png?ex=658dab70&is=657b3670&hm=c4dd168c7ae7252d4da6d53868f151fee5b9a8da544ecd7f3df0360467d55ffe&)

# Novalife Plugin MyMenu

Ce plugin Novalife MyMenu fournit la base d'un menu sur lequel les différents développeurs de plugins peuvent implémenter leurs sections respectives.
L'objectif est d'utiliser le même menu afin d'éviter les conflits potentiels entre nos plugins.

## Table des Matières

- [Installation](#installation)
- [Utilisation](#utilisation)
- [Fonctionnalités](#fonctionnalités)
- [Droits de Propriété Intellectuelle](#droits-de-propriété-intellectuelle)

## Installation

1. Téléchargez le fichier `MyMenu.dll` depuis la page des releases de ce dépôt. 
2. Ajoutez le fichier `MyMenu.dll` dans le dossier des plugins de votre serveur Novalife.

## Utilisation

Le Plugin MyMenu instaure la base d'un menu commun.  
Pour l'utiliser, suivez ces étapes :

1. Intégrez le Plugin MyMenu.dll en tant que référence dans votre projet Novalife.

## Fonctionnalités

Placez votre code à l'intérieur de la fonction OnPluginInit.

```csharp
public override void OnPluginInit()
{
	// Commencer par créer une section en renseignant la version de votre plugin et votre pseudo
	Section section = new Section(Section.GetSourceName(), Section.GetSourceName(), "v0.0.0", "Author");

	// Créer une expression lambda prenant UIPanel en paramètre et renseigner votre fonction principale
	Action<UIPanel> action = ui => VotreFonction(section.GetPlayer(ui));

	//-------------------------------------------------------------------

	// (Facultatif) CONDITIONS PAR DÉFAUT:
	// Quels sont les identifiants des sociétés ayant accès à votre section ? (Ne rien indiquer n'applique pas de condition)
	// exemple:
	section.SetBizIdAllowed(1, 4);

	// Quels sont les types de sociétés ayant accès à votre section ? (Ne rien indiquer n'applique pas de condition)
	// exemple:
	section.SetBizTypeAllowed(Activity.Type.Chef, Activity.Type.Electrician);

	// Est-ce que votre section est accessible uniquement par des administrateurs ? (false par défaut)
	// exemple:
	section.OnlyAdmin = true;

	// Quel rang minimum requis d'un administrateur pour accéder à votre section ? (0 par défaut)
	// exemple:
	section.MinAdminLevel = 5;

	//-------------------------------------------------------------------

	// Création de votre TabLine et insertion dans MyMenu (Renseigner true en paramètre de la fonction Insert SI votre plugin est destiné aux administrateurs)
	section.Line = new UITabLine(section.Title, action);
	section.Insert(false);
}
```

## Droits de Propriété Intellectuelle

Je vous demande simplement de respecter le temps que j'ai mis dans ce plugin.  
Merci de ne pas vous approprier le plugin, de ne pas le copier bêtement, et de ne pas faire des trucs étranges avec.

Pour discuter, contactez-moi sur discord: Aarnow  
Serveur discord: https://discord.gg/8j2suEE9Mf