using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.Linq;

///*
[CustomEditor(typeof(ShaderMorph)), CanEditMultipleObjects]
public class ShaderMorphEditor : Editor
{
    SerializedProperty maxParticles;
    SerializedProperty sourceRenderer;
    SerializedProperty targetRenderer;
    SerializedProperty strategy;


    private Type[] _implementations;
    private int _implementationTypeIndex;

    MorphStrategy strategyPicked;
    enum MorphStrategy
    {
        Simple, Explode
    }

    MorphStrategy morphStrategy;

    void OnEnable()
    {
  //      maxParticles = serializedObject.FindProperty("maxParticles");
 //       sourceRenderer = serializedObject.FindProperty("sourceRenderer");
  //      targetRenderer = serializedObject.FindProperty("targetRenderer");
  //      strategy = serializedObject.FindProperty("strategy");
    }

    public override void OnInspectorGUI()
    {
        ShaderMorph testBehaviour = target as ShaderMorph;

        if (testBehaviour == null)
            return;


        if (_implementations == null || GUILayout.Button("Refresh implementations"))
        {
            //this is probably the most imporant part:
            //find all implementations of INode using System.Reflection.Module
            _implementations = GetImplementations<ShaderMorphStrategy>().Where(impl => !impl.IsSubclassOf(typeof(UnityEngine.Object))).ToArray();
        }

        GUI.changed = false;
        //select implementation from editor popup
        _implementationTypeIndex = EditorGUILayout.Popup(new GUIContent("Implementation"),
            _implementationTypeIndex, _implementations.Select(impl => impl.FullName).ToArray());

        if(GUI.changed)
        {
            testBehaviour.strategy = (ShaderMorphStrategy)Activator.CreateInstance(_implementations[_implementationTypeIndex]);

        }

        base.OnInspectorGUI();
    }
    private static Type[] GetImplementations<T>()
    {
        var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());

        var interfaceType = typeof(T);
        return types.Where(p => interfaceType.IsAssignableFrom(p) && !p.IsAbstract).ToArray();
    }

    /*
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(maxParticles);
        EditorGUILayout.PropertyField(sourceRenderer);
        EditorGUILayout.PropertyField(targetRenderer);

        MorphStrategy newStrategy = (MorphStrategy)EditorGUILayout.EnumPopup("mode:", strategyPicked);
        if(strategyPicked != newStrategy)
        {
            strategyPicked = newStrategy;
         //   if (strategyPicked == MorphStrategy.Explode)
        //        strategy = new ExplodeMorphStrategy();
        }
        EditorGUILayout.PropertyField(strategy, true);
        //       DisplayStrategy();
        serializedObject.ApplyModifiedProperties();
    }

    void DisplayStrategy()
    {
        switch(strategyPicked)
        {
            case MorphStrategy.Simple:
                EditorGUILayout.PropertyField(strategy);

                break;
            case MorphStrategy.Explode:
                break;
            default:
                Debug.LogError("Unrecognized Option");
                break;
        }
    }
    */
}
//*/
