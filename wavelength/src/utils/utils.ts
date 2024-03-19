import resolveConfig from "tailwindcss/resolveConfig";
import tailwindConfig from "../../tailwind.config.ts";

/**
 * A utility type where each key is a class name and each value represents whether that
 * key should be applied to a full class name.
 */
type ConditionalClassName = {
  [className: string]: boolean;
};

type ClassName = string | undefined | ConditionalClassName;

/**
 * Utility function to create a class name for a component. User can either pass a string, undefined, or a ConditionalClassName object.
 * @param classNames the class name parameters the user would like to concatenate together.
 * @returns a single string class name.
 */
export function cn(...classNames: ClassName[]): string {
  const returnedClassNames: string[] = [];

  classNames
    .filter((className) => !!className)
    .forEach((className) => {
      if (typeof className === "string") {
        returnedClassNames.push(className);
      } else {
        const conditional = className as ConditionalClassName;

        Object.keys(conditional).forEach((key: keyof ConditionalClassName) => {
          if (conditional[key]) {
            returnedClassNames.push(key as string);
          }
        });
      }
    });

  return returnedClassNames.join(" ");
}

export const tailWindConfig = resolveConfig(tailwindConfig).theme;
